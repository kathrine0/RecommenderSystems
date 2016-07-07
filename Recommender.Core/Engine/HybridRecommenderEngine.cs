using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using System;

namespace Recommender.Core.Engine
{
    public class HybridRecommenderEngine : RecommenderEngine
    {

        public HybridRecommenderEngine(RecommenderEngine engine) : base(engine)
        { }

        public HybridRecommenderEngine(Logger logger) : base(logger)
        { }

        public override void PrepareSets()
        {
            throw new NotImplementedException();
        }
    }
}
