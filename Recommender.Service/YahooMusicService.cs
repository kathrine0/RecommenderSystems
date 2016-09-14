using MyMediaLite.Data;
using Recommender.Common.Logger;
using Recommender.DataAccess.YahooMusic;
using Recommender.DataAccess.YahooMusic.Entities;
using Recommender.Service.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace Recommender.Service
{
    public class YahooMusicService : RatingService<Rating>, IRatingService
    {
        //todo dependency injection
        YahooMusicContext _context;

        public YahooMusicService()
        {
            _context = new YahooMusicContext();
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

            var groupedRatings = _context.Ratings.Include(x => x.Song)
                                .GroupBy(x => x.UserId)
                                .Where(x => x.Count() > minimumItemsRated)
                                .OrderBy(x => x.Key)
                                .Take(numberOfUsers);

            return PrepareFeaturedSets(groupedRatings, token);
        }

        protected override IRatings CreateBasicSet(IEnumerable<Rating> set, CancellationToken token, double progressStep)
        {
            var ratings = new Ratings();

            set.ToList().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                ratings.Add(x.UserId, x.SongId, x.TheRating);
                Logger.IncrementProgress(progressStep);
            });

            return ratings;
        }


        protected override IFeaturedRatings CreateFeaturedSet(IEnumerable<Rating> set, CancellationToken token, double progressStep)
        {
            var ratings = new FeaturedRatings();

            set.ToList().ForEach(x =>
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                var itemFeatures = new Dictionary<string, object>()
                {
                    { "album", x.Song.AlbumId },
                    { "main_genre", x.Song.Genre.GenreName },
                    { "genre_tree", GetGenreTree(x.Song) }
                };

                ratings.Add(x.UserId, x.SongId, x.TheRating, itemFeatures);
                Logger.IncrementProgress(progressStep);
            });

            return ratings;
        }

        private List<string> GetGenreTree(Song song)
        {
            var genre = song.Genre;

            var genreList = new List<string>();

            while (true)
            {
                genreList.Add(genre.GenreName);

                if (!genre.ParentGenreId.HasValue ||
                    genre.ParentGenreId == 0 ||
                    genre.GenreId == genre.ParentGenreId)
                    break;

                genre = genre.ParentGenre;
            }

            return genreList;
        }

    }
}
