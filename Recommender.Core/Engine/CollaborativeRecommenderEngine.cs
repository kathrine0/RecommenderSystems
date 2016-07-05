using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using System.ComponentModel.DataAnnotations;

namespace Recommender.Core.Engine
{
    public class CollaborativeRecommenderEngine : RecommenderEngine
    {
        public CollaborativeRecommenderEngine(Logger logger) : base(logger)
        {

        }

        public override void LoadData()
        {
            _service.LoadBasicData(out _trainingData, out _testData, _trainingSetRatio);
        }
    }
}
