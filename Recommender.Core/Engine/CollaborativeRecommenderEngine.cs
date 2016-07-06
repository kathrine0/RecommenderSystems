using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using System.ComponentModel.DataAnnotations;

namespace Recommender.Core.Engine
{
    public class CollaborativeRecommenderEngine : RecommenderEngine
    {
        public CollaborativeRecommenderEngine(RecommenderEngine engine) : base(engine)
        {

        }

        //public override void PrepareSets()
        //{
        //    _service.LoadBasicData(out _trainingData, out _testData, TrainingSetRatio);
        //}
    }
}
