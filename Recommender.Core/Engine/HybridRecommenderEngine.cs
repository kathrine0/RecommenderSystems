using Recommender.Service.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.RatingPrediction.Hybrid;
using Recommender.Service.Data;
using System.Threading;
using Recommender.Core.Engine;
using MyMediaLite.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recommender.Core.Engine
{
    public class HybridRecommenderEngine : RecommenderEngine
    {
        public override RatingPrediction.IRatingPredictor Recommender
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
            var reportText = string.Format(@"
Loading data:
    Data type: SIMPLE & FEATURED
    Number of users (simple): {0} 
    Number of users (featured): {1} 
    Minimum rated items: {2}
    Ratio: {3}
", BasicDataUsersQuantity, FeaturedDataUsersQuantity, MinimumItemsRated, Crossvalidation);


            Logger.AddProgressReport(new ProgressState(0, reportText, "Loading data..."));

            _service.LoadComplexData(out _featuredData, out _data, FeaturedDataUsersQuantity, BasicDataUsersQuantity, MinimumItemsRated, token);
        
        }

        public override Dictionary<string, float> TestRecommender(CancellationToken token)
        {
            var result = new Dictionary<string, float>() {
                { "RMSE", 0 },
                { "MAE", 0 },
                { "CBD", 0 }
            };

            var simpleSplit = new Engine.RatingCrossValidationSplit(_data, Crossvalidation);
            var featuredSplit = new Engine.RatingCrossValidationSplit(_featuredData, Crossvalidation);

            int i = 0;

            Recommender.LogTrainining();

            for (i = 0; i < simpleSplit.NumberOfFolds; i++)
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                Logger.AddProgressReport(new ProgressState(40, null, "Learning: fold " + (i + 1)));

                ((IHybridPredictor)_recommender).CollaborativeRatings = simpleSplit.Train[i];
                ((IHybridPredictor)_recommender).ContentRatings = (IFeaturedRatings) featuredSplit.Train[i] ;

                var teachingResult = TeachRecommender();

                if (teachingResult)
                {
                    IncrementResults(result, Recommender.Evaluate(featuredSplit.Test[i]));
                }
            }

            AdjustResults(result, i);
            PublishResults(result);

            return result;
        }
    }
}
