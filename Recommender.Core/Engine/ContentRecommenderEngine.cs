using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.RatingPrediction.ContentBased;
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
                if (!(value is IFeaturedPredictor))
                    throw new System.InvalidCastException("recommender must be IFeaturedPredictor");

                _recommender = value;
                AssignLogger(_recommender);
            }
        }

        public override void PrepareSets(CancellationToken token)
        {
            var reportText = string.Format(@"
Loading data:
    Data type: FEATURED
    Number of users: {0} 
    Minimum rated items: {1}
    Ratio: {2}/{3}
", FeaturedDataUsersQuantity, MinimumItemsRated, TrainingSetRatio * 100, 100 - TrainingSetRatio * 100);


            Logger.AddProgressReport(new ProgressState(0, reportText, "Loading data..."));

            _service.LoadFeaturedData(out _trainingData, out _testData, TrainingSetRatio, FeaturedDataUsersQuantity, MinimumItemsRated, token);
        }
    }
}
