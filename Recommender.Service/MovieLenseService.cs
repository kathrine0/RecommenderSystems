using System.Linq;
using System;
using MyMediaLite.Data;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.MovieLense.Entities;
using System.Collections.Generic;
using Recommender.Service.DTO;
using AutoMapper;

namespace Recommender.Service
{
    public class MovieLenseService : IRatingService
    {
        //todo dependency injection
        MovieLenseContext _context;

        public MovieLenseService(MovieLenseContext context)
        {
            _context = context;
        }

        //todo remove me
        public MovieLenseService()
        {
            _context = new MovieLenseContext();
        }
        
        public IEnumerable<RatingDTO> GetTestSet(double percentage = 0.2)
        {
            return GetRatingLimitedSet(0.2, true);
        }

        public Ratings GetLearningSetMediaLiteFormat(double percentage = 0.8)
        {
            var ratings = new Ratings();
            var set = GetRatingLimitedSet(0.8, false).ToList();

            set.ForEach(x => {
                ratings.Add(x.UserId, x.ItemId, x.Rating);
            });

            return ratings;
        }

        public Ratings GetTestSetMediaLiteFormat(double percentage = 0.2)
        {
            var ratings = new Ratings();
            var set = GetRatingLimitedSet(0.2, true).ToList();

            set.ForEach(x => {
                ratings.Add(x.UserId, x.ItemId, x.Rating);
            });

            return ratings;
        }

        private IEnumerable<RatingDTO> GetRatingLimitedSet(double percentage, bool fromEnd)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentOutOfRangeException("percentage should be in range from 0 to 1");

            var count = _context.Ratings.Count() * 0.1; //todo change me

            int take = (int) Math.Floor(count * percentage);
            int skip = fromEnd ? (int) Math.Ceiling(count * (1 - percentage)) : 0;

            var result = _context.Ratings.OrderBy(x => x.Timestamp).Skip(skip).Take(take);

            var ratings = result.AsEnumerable();
            return ratings.MapTo<RatingDTO>();
        }
    }
}
