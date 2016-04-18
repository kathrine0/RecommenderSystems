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

        public void LoadFeaturedData(out IRatings _trainingData, out IRatings _testData, double ratio)
        {
            if (ratio < 0 || ratio > 1)
                throw new ArgumentOutOfRangeException("ratio should be in range from 0 to 1");

            _trainingData = (IFeaturedRatings) GetFeaturedSet(ratio, false);
            _testData = (IFeaturedRatings) GetFeaturedSet(1 - ratio, true);
        }

        private IRatings GetBasicSet(double percentage, bool fromEnd)
        {
            var ratings = new Ratings();
            var set = GetRatingLimitedSet(percentage, fromEnd).MapTo<RatingDTO>();

            set.ForEach(x => {
                ratings.Add(x.UserId, x.ItemId, x.Rating);
            });

            return ratings;
        }

        private IFeaturedRatings GetFeaturedSet(double percentage, bool fromEnd)
        {
            var ratings = new FeaturedRatings();
            var set = GetRatingLimitedSet(percentage, fromEnd).MapTo<RatingWithFeaturesDTO>();

            set.ForEach(x => {
                ratings.Add(x.UserId, x.ItemId, x.Rating, x.ItemFeatures);
            });

            return ratings;
        }

        private IEnumerable<Rating> GetRatingLimitedSet(double percentage, bool fromEnd)
        {
            var count = _context.Ratings.Count() * 0.1; //todo change me

            int take = (int)Math.Floor(count * percentage);
            int skip = fromEnd ? (int)Math.Ceiling(count * (1 - percentage)) : 0;

            var result = _context.Ratings.OrderBy(x => x.Timestamp).Skip(skip).Take(take);

            var ratings = result.AsEnumerable();
            return ratings;
        }
    }
}
