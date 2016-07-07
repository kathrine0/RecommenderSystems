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

        public override bool TeachRecommender()
        {
            if (!DataLoaded)
            {
                Logger.AddWarningReport(new WarningReport("No data loaded"));
                return false;
            }

            Recommender.Ratings = TrainingData;
            if (Recommender == null)
            {
                Logger.AddWarningReport(new WarningReport("Recommender not set"));
                return false;
            }

            // set up the recommender

            Recommender.Train();

            return ((NeuroRecommender)Recommender).RecommenderStatus;
        }
    }
}
