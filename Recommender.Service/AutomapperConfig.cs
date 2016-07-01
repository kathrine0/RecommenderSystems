using AutoMapper;
using Recommender.DataAccess.MovieLense.Entities;
using Recommender.Service.DTO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Recommender.Service
{
    public static class AutomapperConfig
    {
        public static void CreateMaps()
        {
            MovieLenseAutomapperConfig.CreateMaps();
        }
    }

    //todo move me somewhere else
    public static class MovieLenseAutomapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<Rating, RatingDTO>()
                .IgnoreAllNonExisting()
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(x => x.UserId.Value))
                .ForMember(dst => dst.ItemId, opt => opt.MapFrom(x => x.MovieId.Value))
                .ForMember(dst => dst.Rating, opt => opt.MapFrom(x => x.TheRating));

            Mapper.CreateMap<Rating, RatingWithFeaturesDTO>()
                .IgnoreAllNonExisting()
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(x => x.UserId.Value))
                .ForMember(dst => dst.ItemId, opt => opt.MapFrom(x => x.MovieId.Value))
                .ForMember(dst => dst.Rating, opt => opt.MapFrom(x => x.TheRating))
                .ForMember(dst => dst.ItemFeatures, opt => opt.MapFrom(
                    x => new Dictionary<string, object>()
                    {
                        //{ "title", x.Movie.Title },
                        { "director", x.Movie.Director },
                       // { "year", x.Movie.Year.ToString() }, //TODO: remove toString here and parse is as int
                        { "language", x.Movie.Language },
                        { "country", x.Movie.Country },
                        { "actors", x.Movie.Actors },
                        { "genres", x.Movie.Genres },
                        //{ "imdbRating", x.Movie.ImdbRating.ToString() }
                    }));


            Mapper.CreateMap<Movie, ItemDTO>()
                .IgnoreAllNonExisting()
                .ForMember(dst => dst.ItemFeatures, opt => opt.MapFrom(
                    x => new List<string>()
                    {
                        x.Title,
                        x.Director,
                        x.Year.ToString(),
                        x.Language,
                        x.Country,
                        x.Actors,
                        x.Genres,
                        x.ImdbRating.ToString()
                    }));
        }
    }
}
