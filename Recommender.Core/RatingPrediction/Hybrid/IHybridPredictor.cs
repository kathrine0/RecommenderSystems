using MyMediaLite.Data;
using MyMediaLite.RatingPrediction;
using Recommender.Core.RatingPrediction.ContentBased;
using Recommender.Service.Data;

namespace Recommender.Core.RatingPrediction.Hybrid
{
    public interface IHybridPredictor : IFeaturedPredictor
    {
        IRatingPredictor CollaborativeRecommender { get; set; }

        IFeaturedPredictor ContentRecommender { get; set; }

        IRatings CollaborativeRatings { get; set; }
        IFeaturedRatings ContentRatings { get; set; }
    }
}