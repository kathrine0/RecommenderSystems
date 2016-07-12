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
            var reportText = "    Data type: SIMPLE\n";
            Logger.AddProgressReport(new ProgressState(1, reportText, null));

            _service.LoadBasicData(out _trainingData, out _testData, TrainingSetRatio, NumberOfUsers, MinimumItemsRated, token);
        }
    }
}
