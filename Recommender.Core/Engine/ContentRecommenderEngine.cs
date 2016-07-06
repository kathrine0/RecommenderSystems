using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.MachineLearning;

namespace Recommender.Core.Engine
{
    public class ContentRecommenderEngine : RecommenderEngine
    {
        public ContentRecommenderEngine(RecommenderEngine engine) : base(engine)
        {

        }

        public override IRatingPredictor Recommender
        {
            get { return _recommender; }
            set
            {
                if (value is IFeaturedPredictor)
                    _recommender = value;
                else
                    throw new System.InvalidCastException("recommender must be IFeaturedPredictor");
            }
        }

        //public override void PrepareSets()
        //{
        //    _service.LoadFeaturedData(out TrainingData, out TestData, _trainingSetRatio, NumberOfUsers, MinimumItemsRated);
        //}
    }
}
