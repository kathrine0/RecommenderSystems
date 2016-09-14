using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.RatingPrediction.ContentBased;
using Recommender.Service.Data;
using System.Collections.Generic;
using System.Threading;

namespace Recommender.Core.Engine
{
    public class ContentRecommenderEngine : RecommenderEngine
    {
        public ContentRecommenderEngine(RecommenderEngine engine) : base(engine)
        { }

        public ContentRecommenderEngine(Logger logger) : base(logger)
        { }

        public override RatingPrediction.IRatingPredictor Recommender
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
    DoCrossValidation: {2}
", FeaturedDataUsersQuantity, MinimumItemsRated, Crossvalidation);


            Logger.AddProgressReport(new ProgressState(0, reportText, "Loading data..."));

            FeaturedData = _service.LoadFeaturedData(FeaturedDataUsersQuantity, MinimumItemsRated, token);
        }
    }
}
