using Recommender.Service.Data;
using System.Collections.Generic;

namespace Recommender.Core.RatingPrediction.ContentBased
{
    public interface IFeaturedPredictor : IRatingPredictor
    {
        /// <summary>training data that also contains the features information</summary>
        IFeaturedRatings FeaturedRatings { get; set; }

        /// <summary>predict rating at a certain point in time</summary>
        /// <param name='user_id'>the user ID</param>
        /// <param name='item_id'>the item ID</param>
        /// <param name='features'>the features of the rated item</param>
        float Predict(int user_id, int item_id, IList<IFeature> features);

        double GetError(int user_id);
    }
}