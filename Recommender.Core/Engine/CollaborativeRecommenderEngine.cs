using MyMediaLite.RatingPrediction;

namespace Recommender.Core.Engine
{
    public class CollaborativeRecommenderEngine : RecommenderEngine
    {
        public override void LoadData()
        {
            _service.LoadBasicData(out _trainingData, out _testData, 0.8);
        }
    }
}
