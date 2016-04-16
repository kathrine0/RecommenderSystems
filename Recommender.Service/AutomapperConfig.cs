using AutoMapper;
using Recommender.DataAccess.MovieLense.Entities;
using Recommender.Service.DTO;
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
        }
    }
}
