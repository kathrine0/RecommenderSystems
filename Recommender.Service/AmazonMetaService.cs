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
    public class AmazonMetaService : RatingService, IRatingService
    {
        //todo dependency injection
        AmazonMetaContext _context;

        public AmazonMetaService()
        {
            _context = new AmazonMetaContext();
        }


        protected override IRatings[] GetComplexSets(double percentage, int numberOfUsers, int numberForSimpleData, int minimumItemsRated, CancellationToken token)
        {
            var simpleTrainRatings = new Ratings();
            var featuredTrainRatings = new FeaturedRatings();
            var featuredTestRatings = new FeaturedRatings();

            ///get data for content-based
            var groupedRatings = _context.Ratings.Include(x => x.Product)
                                .GroupBy(x => x.CustomerId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);

            var sets = SplitSets(percentage, groupedRatings, token);

            double progressStep = (double)50 / (double)(sets[0].Count() + sets[1].Count());

            CreateFeaturedSet(sets[0], featuredTrainRatings, token, progressStep);
            CreateBasicSet(sets[0], simpleTrainRatings, token, progressStep);

            CreateFeaturedSet(sets[1], featuredTestRatings, token, progressStep);

            ///get data for collaborative
            var simpleGroupedRatings = _context.Ratings
                                .GroupBy(x => x.CustomerId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Skip(numberOfUsers)
                                .Take(numberForSimpleData - numberOfUsers);

            var simpleSets = SplitSets(1, simpleGroupedRatings, token);

            double progressStep2 = (double)50 / (double)(simpleSets[0].Count());

            CreateBasicSet(simpleSets[0], simpleTrainRatings, token, progressStep2);

            return new IRatings[3] { featuredTrainRatings, featuredTestRatings, simpleTrainRatings };

        }

        protected override IRatings[] GetBasicSets(double percentage, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            var trainRatings = new Ratings();
            var testRatings = new Ratings();

            var groupedRatings = _context.Ratings
                                .GroupBy(x => x.CustomerId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);

            var sets = SplitSets(percentage, groupedRatings, token);
            double progressStep = (double)50 / (double)(sets[0].Count() + sets[1].Count());

            CreateBasicSet(sets[0], trainRatings, token, progressStep);
            CreateBasicSet(sets[1], testRatings, token, progressStep);

            return new IRatings[2] { trainRatings, testRatings };

        }

        private void CreateBasicSet(IEnumerable<Rating> set, Ratings ratings, CancellationToken token, double progressStep)
        {
            set.ToList().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                ratings.Add(x.CustomerId, x.ProductId, x.ReviewRatingNumber.Value);
                Logger.IncrementProgress(progressStep);
            });
        }

        protected override IFeaturedRatings[] GetFeaturedSets(double percentage, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            var trainRatings = new FeaturedRatings();
            var testRatings = new FeaturedRatings();

            var groupedRatings = _context.Ratings.Include(x => x.Product)
                                .GroupBy(x => x.CustomerId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);

            var sets = SplitSets(percentage, groupedRatings, token);

            double progressStep = (double)50 / (double)(sets[0].Count() + sets[1].Count());

            CreateFeaturedSet(sets[0], trainRatings, token, progressStep);
            CreateFeaturedSet(sets[1], testRatings, token, progressStep);

            return new IFeaturedRatings[2] { trainRatings, testRatings };
        }

        private void CreateFeaturedSet(IEnumerable<Rating> set, FeaturedRatings ratings, CancellationToken token, double progressStep)
        {
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
        }

        private IEnumerable<Rating>[] SplitSets(double percentage, IQueryable<IGrouping<int, Rating>> groupedRatings, CancellationToken token)
        {
            double progressStep = (double)50 / (double)groupedRatings.Count();
            //od 0 do 50 --- 50 jednostek

            var trainingSet = new List<Rating>();
            var testingSet = new List<Rating>();

            foreach (var groupedRating in groupedRatings)
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);


                var count = groupedRating.Count();

                int trainingTake = (int)Math.Floor(count * percentage);
                int testingTake = count - trainingTake;

                trainingSet.AddRange(groupedRating
                                            .Take(trainingTake));
                testingSet.AddRange(groupedRating
                                            .Skip(trainingTake)
                                            .Take(testingTake));

                Logger.IncrementProgress(progressStep);
            }

            return new List<Rating>[2] { trainingSet, testingSet };
        }

    }
}
