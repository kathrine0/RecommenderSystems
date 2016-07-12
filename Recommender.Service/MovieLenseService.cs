using System.Linq;
using System;
using MyMediaLite.Data;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.MovieLense.Entities;
using System.Collections.Generic;
using Recommender.Service.DTO;
using System.Data.Entity;
using Recommender.Service.Data;
using System.Threading;
using Recommender.Common.Logger;

namespace Recommender.Service
{
    public class MovieLenseService : IRatingService
    {

        public Logger Logger { get; set; }
        //todo dependency injection
        MovieLenseContext _context;

        public MovieLenseService()
        {
            _context = new MovieLenseContext();
        }

        //LOAD BY USER

        public void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            var sets = GetBasicSets(ratio, numberOfUsers, minimumItemsRated, token);

            _trainingData = sets[0];
            _testData = sets[1];
        }

        public void LoadFeaturedData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            var sets = GetFeaturedSets(ratio, numberOfUsers, minimumItemsRated, token);

            _trainingData = sets[0];
            _testData = sets[1];
        }




        private IRatings[] GetBasicSets(double percentage, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            var trainRatings = new Ratings();
            var testRatings = new Ratings();

            var groupedRatings = _context.Ratings
                                .GroupBy(x => x.UserId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .Take(numberOfUsers);
            
            var sets = SplitSets(percentage, groupedRatings, token);

            sets[0].MapTo<RatingDTO>().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                trainRatings.Add(x.UserId, x.ItemId, x.Rating);
            });

            sets[1].MapTo<RatingDTO>().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                testRatings.Add(x.UserId, x.ItemId, x.Rating);
            });

            return new IRatings[2] { trainRatings, testRatings };

        }

        private IFeaturedRatings[] GetFeaturedSets(double percentage, int numberOfUsers, int minimumItemsRated, CancellationToken token)
        {
            var trainRatings = new FeaturedRatings();
            var testRatings = new FeaturedRatings();

            var groupedRatings = _context.Ratings.Include(x => x.Movie)
                                .GroupBy(x => x.UserId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .Take(numberOfUsers);

            var sets = SplitSets(percentage, groupedRatings, token);

            double progressStep = (double) 50 / (double)(sets[0].Count() + sets[1].Count());

            sets[0].MapTo<RatingWithFeaturesDTO>().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                trainRatings.Add(x.UserId, x.ItemId, x.Rating, x.ItemFeatures);
                Logger.IncrementProgress(progressStep);
            });

            sets[1].MapTo<RatingWithFeaturesDTO>().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                testRatings.Add(x.UserId, x.ItemId, x.Rating, x.ItemFeatures);
                Logger.IncrementProgress(progressStep);
            });

            return new IFeaturedRatings[2] { trainRatings, testRatings };
        }

        private IEnumerable<Rating>[] SplitSets(double percentage, IQueryable<IGrouping<int?, Rating>> groupedRatings, CancellationToken token)
        {
            double progressStep = (double) 50 / (double) groupedRatings.Count();
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


        //LOAD ALL

        public void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            _trainingData = GetBasicSet(ratio, false);
            _testData = GetBasicSet(1 - ratio, true);
        }

        private IRatings GetBasicSet(double percentage, bool fromEnd)
        {
            var ratings = new Ratings();
            var set = GetRatingLimitedSets(percentage, fromEnd).MapTo<RatingDTO>();

            set.ForEach(x => {
                ratings.Add(x.UserId, x.ItemId, x.Rating);
            });

            return ratings;
        }

        private IEnumerable<Rating> GetRatingLimitedSets(double percentage, bool fromEnd)
        {
            var count = _context.Ratings.Count();

            int take = (int)Math.Floor(count * percentage);
            int skip = fromEnd ? (int)Math.Ceiling(count * (1 - percentage)) : 0;

            var result = _context.Ratings.OrderBy(x => x.Timestamp).Skip(skip).Take(take);

            var ratings = result.AsEnumerable();
            return ratings;
        }
    }
}
