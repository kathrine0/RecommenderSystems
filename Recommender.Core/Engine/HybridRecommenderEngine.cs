using MyMediaLite.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.RatingPrediction.ContentBased;
using Recommender.Core.RatingPrediction.Hybrid;
using Recommender.Service.Data;
using System.Threading;

namespace Recommender.Core.Engine
{
    public class HybridRecommenderEngine : RecommenderEngine
    {
        protected IRatings _collaborativeTrainingData;
        protected IRatings _collaborativeTestData;

        protected IRatings _contentTrainingData;
        protected IRatings _contentTestData;

        public override IRatingPredictor Recommender
        {
            get { return _recommender; }
            set
            {
                if (!(value is IHybridPredictor))
                    throw new System.InvalidCastException("recommender must be IHybridPredictor");

                _recommender = value;
                AssignLogger(_recommender);
            }
        }


        public HybridRecommenderEngine(RecommenderEngine engine) : base(engine)
        { }

        public HybridRecommenderEngine(Logger logger) : base(logger)
        { }

        public override void PrepareSets(CancellationToken token)
        {
            var reportText = "    Data type: SIMPLE & FEATURED\n";
            Logger.AddProgressReport(new ProgressState(1, reportText, null));

            _service.LoadBasicData(out _collaborativeTrainingData, out _collaborativeTestData, TrainingSetRatio, AmountOfUsersTrain, MinimumItemsRated, token);

            _service.LoadFeaturedData(out _contentTrainingData, out _contentTestData, TrainingSetRatio, AmountOfUsersTest, MinimumItemsRated, token);

            ((IHybridPredictor)_recommender).CollaborativeRatings = _collaborativeTrainingData;
            ((IHybridPredictor)_recommender).ContentRatings = (IFeaturedRatings) _contentTrainingData;
            TestData = _contentTestData;

        }

        public override RatingPredictionEvaluationResults GetResults()
        {
            Logger.AddProgressReport(new ProgressState(90, "", "Evaluating results..."));
            // measure the accuracy on the test data set
            var result = Recommender.Evaluate(TestData);

            var resString = result.ToString() + "\n =========================================";

            Logger.AddProgressReport(new ProgressState(100, resString, "Finished"));

            return result;
        }
    }
}
