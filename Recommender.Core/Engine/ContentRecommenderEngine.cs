using MyMediaLite.RatingPrediction;
using Recommender.Core.MachineLearning;

namespace Recommender.Core.Engine
{
    public class ContentRecommenderEngine : RecommenderEngine
    {
        //todo make Recommender obligatry to be of type IFeaturedPredictor

        public override void LoadData()
        {
            _service.LoadFeaturedData(out _trainingData, out _testData, 0.8);
        }
    }
}
