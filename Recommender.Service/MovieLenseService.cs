using System.Linq;
using System;
using MyMediaLite.Data;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.MovieLense.Entities;

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

        public IQueryable<Rating> GetAll()
        {
            return _context.Ratings.AsQueryable();
        }

        public IQueryable<Rating> GetLearningSet(double percentage = 0.8)
        {
            return GetLimitedSet(0.8, false);
        }

        public IQueryable<Rating> GetTestSet(double percentage = 0.2)
        {
            return GetLimitedSet(0.2, true);
        }

        public Ratings GetLearningSetMediaLiteFormat(double percentage = 0.8)
        {
            var ratings = new Ratings();
            var set = GetLimitedSet(0.8, false).ToList();

            set.ForEach(x => {
                ratings.Add(x.UserId.Value, x.MovieId.Value, x.TheRating);
            });

            return ratings;
        }

        public Ratings GetTestSetMediaLiteFormat(double percentage = 0.2)
        {
            var ratings = new Ratings();
            var set = GetLimitedSet(0.2, true).ToList();

            set.ForEach(x => {
                ratings.Add(x.UserId.Value, x.MovieId.Value, x.TheRating);
            });

            return ratings;
        }

        private IQueryable<Rating> GetLimitedSet(double percentage, bool fromEnd)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentOutOfRangeException("percentage should be in range from 0 to 1");

            var count = _context.Ratings.Count() * 0.1; //todo change me

            int take = (int) Math.Floor(count * percentage);
            int skip = fromEnd ? (int) Math.Ceiling(count * (1 - percentage)) : 0;

            var result = _context.Ratings.OrderBy(x => x.Timestamp).Skip(skip).Take(take);

            return result.AsQueryable();
        }


    }
}
