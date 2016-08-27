using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Recommender.Core.Engine
{
    public class CollaborativeRecommenderEngine : RecommenderEngine
    {
        public CollaborativeRecommenderEngine(RecommenderEngine engine) : base(engine)
        { }

        public CollaborativeRecommenderEngine(Logger logger) : base(logger)
        { }

        public override void PrepareSets(CancellationToken token)
        {
            var reportText = string.Format(@"
Loading data:
    Data type: SIMPLE
    Number of users: {0} 
    Minimum rated items: {1}
    Ratio: {2}/{3}
", BasicDataUsersQuantity, MinimumItemsRated, TrainingSetRatio * 100, 100 - TrainingSetRatio * 100);


            Logger.AddProgressReport(new ProgressState(0, reportText, "Loading data..."));

            _service.LoadBasicData(out _trainingData, out _testData, TrainingSetRatio, BasicDataUsersQuantity, MinimumItemsRated, token);
        }
    }
}
