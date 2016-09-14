using MyMediaLite.Data;
using Recommender.Common.Logger;
using Recommender.DataAccess.AmazonMeta;
using Recommender.DataAccess.AmazonMeta.Entities;
using Recommender.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Recommender.Service
{
    public class AmazonMetaService : RatingService<Rating>, IRatingService
    {
        //todo dependency injection
        AmazonMetaContext _context;

        public AmazonMetaService()
        {
            _context = new AmazonMetaContext();
        }


        protected override IRatings GetBasicSet(int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {

            var groupedRatings = _context.Ratings
                                .GroupBy(x => x.CustomerId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);

            
            return PrepareBasicSets(groupedRatings, token);

        }
        protected override IFeaturedRatings GetFeaturedSet(int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {

            var groupedRatings = _context.Ratings.Include(x => x.Product)
                                .GroupBy(x => x.CustomerId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);

            return PrepareFeaturedSets(groupedRatings, token);
        }

        protected override IRatings CreateBasicSet(IEnumerable<Rating> set, CancellationToken token, double progressStep)
        {
            var ratings = new Ratings();

            set.ToList().ForEach((Action<Rating>)((Rating x) =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                ratings.Add(((Rating)x).CustomerId, x.ProductId, x.ReviewRatingNumber.Value);
                Logger.IncrementProgress(progressStep);
            }));

            return ratings;
        }

        protected override IFeaturedRatings CreateFeaturedSet(IEnumerable<Rating> set, CancellationToken token, double progressStep)
        {
            var ratings = new FeaturedRatings();

            set.ToList().ForEach((Action<Rating>)((Rating x) =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                if (!x.ReviewRatingNumber.HasValue)
                    return;

                var itemFeatures = new Dictionary<string, object>()
                {
                    { "group_desc", x.Product.ProductGroupDescription },
                    { "categories", x.Product.Categories },
                };

                ratings.Add(x.CustomerId, x.ProductId, x.ReviewRatingNumber.Value, itemFeatures);
                Logger.IncrementProgress(progressStep);
            }));

            return ratings;
        }

  
    }
}
