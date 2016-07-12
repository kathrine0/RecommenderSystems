using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.MachineLearning;
using System.Threading;

namespace Recommender.Core.Engine
{
    public class ContentRecommenderEngine : RecommenderEngine
    {
        public ContentRecommenderEngine(RecommenderEngine engine) : base(engine)
        { }

        public ContentRecommenderEngine(Logger logger) : base(logger)
        { }

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

        public override void PrepareSets(CancellationToken token)
        {
            var reportText = "    Data type: FEATURED\n";
            Logger.AddProgressReport(new ProgressState(1, reportText, null));

            _service.LoadFeaturedData(out _trainingData, out _testData, TrainingSetRatio, NumberOfUsers, MinimumItemsRated, token);
        }
    }
}
