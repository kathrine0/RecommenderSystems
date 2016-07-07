using System.Linq;
using System;
using MyMediaLite.Data;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.MovieLense.Entities;
using System.Collections.Generic;
using Recommender.Service.DTO;
using AutoMapper;
using Recommender.Service.Data;

namespace Recommender.Service
{
    public class MovieLenseService : IRatingService
    {
        //todo dependency injection
        MovieLenseContext _context;

        public MovieLenseService()
        {
            _context = new MovieLenseContext();
        }

        public void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            _trainingData = GetBasicSet(ratio, false);
            _testData = GetBasicSet(1-ratio, true);
        }

        public void LoadFeaturedData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            var sets = GetFeaturedSets(ratio, numberOfUsers, minimumItemsRated);

            _trainingData = sets[0];
            _testData = sets[1];
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

        private IFeaturedRatings[] GetFeaturedSets(double percentage, int numberOfUsers, int minimumItemsRated)
        {
            var trainRatings = new FeaturedRatings();
            var testRatings = new FeaturedRatings();

            var sets = GetRatingSetsByUser(percentage, numberOfUsers, minimumItemsRated);

            sets[0].MapTo<RatingWithFeaturesDTO>().ForEach(x =>
            {
                trainRatings.Add(x.UserId, x.ItemId, x.Rating, x.ItemFeatures);
            });

            sets[1].MapTo<RatingWithFeaturesDTO>().ForEach(x =>
            {
                testRatings.Add(x.UserId, x.ItemId, x.Rating, x.ItemFeatures);
            });

            return new IFeaturedRatings[2] { trainRatings, testRatings };
        }

        private IEnumerable<Rating>[] GetRatingSetsByUser(double percentage, int numberOfUsers, int minimumItemsRated)
        {
            var trainingSet = new List<Rating>();
            var testingSet = new List<Rating>();

            var groupedRatings = _context.Ratings
                                            .GroupBy(x => x.UserId)
                                            .Where(x => x.Count() > minimumItemsRated)
                                            .Take(numberOfUsers);

            foreach (var groupedRating in groupedRatings)
            {
                var count = groupedRating.Count();

                int trainingTake = (int)Math.Floor(count * percentage);
                int testingTake = count - trainingTake;

                trainingSet.AddRange(groupedRating
                                            .Take(trainingTake));
                testingSet.AddRange(groupedRating
                                            .Skip(trainingTake)
                                            .Take(testingTake));
            }
            
            return new List<Rating>[2] { trainingSet, testingSet };
        }

        private IEnumerable<Rating> GetRatingLimitedSets(double percentage, bool fromEnd)
        {
            //FOR DEV PURPOSES GET DATA ONLY FOR FIRST 50 USERS!!
            var limit = 50;

            var count = _context.Ratings.Where(x => x.UserId <= limit).Count();
            //var count = _context.Ratings.Count();

            int take = (int)Math.Floor(count * percentage);
            int skip = fromEnd ? (int)Math.Ceiling(count * (1 - percentage)) : 0;

            var result = _context.Ratings.Where(x => x.UserId <= limit).OrderBy(x => x.Timestamp).Skip(skip).Take(take);
            //var result = _context.Ratings.OrderBy(x => x.Timestamp).Skip(skip).Take(take);

            var ratings = result.AsEnumerable();
            return ratings;
        }
    }
}
