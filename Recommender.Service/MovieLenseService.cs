using System.Linq;
using System;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.MovieLense.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using Recommender.Service.Data;
using System.Threading;
using Recommender.Common.Logger;
using MyMediaLite.Data;

namespace Recommender.Service
{
    public class MovieLenseService : RatingService<Rating>, IRatingService
    {
        //todo dependency injection
        MovieLenseContext _context;

        public MovieLenseService()
        {
            _context = new MovieLenseContext();
        }

        protected override IRatings GetBasicSet(int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {

            var groupedRatings = _context.Ratings
                                .GroupBy(x => x.UserId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);


            return PrepareBasicSets(groupedRatings, token);

        }
        protected override IFeaturedRatings GetFeaturedSet(int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {

            var groupedRatings = _context.Ratings.Include(x => x.Movie)
                                .GroupBy(x => x.UserId)
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

                ratings.Add(x.UserId.Value, x.MovieId.Value, x.TheRating);
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

                var itemFeatures = new Dictionary<string, object>()
                {
                    //{ "title", x.Movie.Title },
                    { "director", x.Movie.Director },
                    // { "year", x.Movie.Year.ToString() }, //TODO: remove toString here and parse is as int
                    { "language", x.Movie.Language },
                    { "country", x.Movie.Country },
                    { "actors", x.Movie.Actors },
                    { "genres", x.Movie.Genres },
                    //{ "imdbRating", x.Movie.ImdbRating.ToString() }
                };

                ratings.Add(x.UserId.Value, x.MovieId.Value, x.TheRating, itemFeatures);
                Logger.IncrementProgress(progressStep);
            }));

            return ratings;
        }

        //LOAD ALL
        //public void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio)
        //{
        //    if (ratio < 0 || ratio > 1)
        //        throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

        //    _trainingData = GetBasicSet(ratio, false);
        //    _testData = GetBasicSet(1 - ratio, true);
        //}

        //private IRatings GetBasicSet(double percentage, bool fromEnd)
        //{
        //    var ratings = new Ratings();
        //    var set = GetRatingLimitedSets(percentage, fromEnd).MapTo<RatingDTO>();

        //    set.ForEach(x => {
        //        ratings.Add(x.UserId, x.ItemId, x.Rating);
        //    });

        //    return ratings;
        //}

        //private IEnumerable<Rating> GetRatingLimitedSets(double percentage, bool fromEnd)
        //{
        //    var count = _context.Ratings.Count();

        //    int take = (int)Math.Floor(count * percentage);
        //    int skip = fromEnd ? (int)Math.Ceiling(count * (1 - percentage)) : 0;

        //    var result = _context.Ratings.OrderBy(x => x.Timestamp).Skip(skip).Take(take);

        //    var ratings = result.AsEnumerable();
        //    return ratings;
        //}
    }
}
