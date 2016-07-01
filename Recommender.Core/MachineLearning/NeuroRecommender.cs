using MyMediaLite.RatingPrediction;
using System;
using MyMediaLite.Data;
using Recommender.Service.Data;
using System.Linq;
using System.Collections.Generic;
using Accord.Math;
using Recommender.Common;

namespace Recommender.Core.MachineLearning
{
    public class NeuroRecommender : RatingPredictor
    {

        public NeuroRecommender()
        {
        }


        public virtual IFeaturedRatings FeaturedRatings
        {
            get { return featured_ratings; }
            set
            {
                featured_ratings = value;
                Ratings = value;
            }
        }
        /// <summary>rating data, including item features</summary>
        protected IFeaturedRatings featured_ratings;

        ///
        public override IRatings Ratings
        {
            get { return ratings; }
            set
            {
                if (!(value is IFeaturedRatings))
                    throw new ArgumentException("Ratings must be of type IFeaturedRatings.");

                base.Ratings = value;
                featured_ratings = (IFeaturedRatings)value;
            }
        }

        protected double weight;


        public override float Predict(int user_id, int item_id)
        {
            throw new NotImplementedException();
        }

        public override void Train()
        {
            InitModel();//ratings.RandomIndex

            throw new NotImplementedException();
        }


        protected internal virtual void InitModel()//IList<int> rating_indices
        {
            //please please please refactor me!
            //move it to featuredRatings class
            foreach (var userRatings in featured_ratings.ByUser)
            {

                //build up the items profiles with features
                var allFeatures = new List<IFeature>();


                if (userRatings.Count == 0)
                    continue;

                int userId = featured_ratings.Users[userRatings.First()];

                var ratedItems = new List<RatedItem>();

                //and items to datatable
                foreach (var index in userRatings)
                {
                    int i = featured_ratings.Items[index];
                    var features = featured_ratings.Features[index];

                    var itemId = featured_ratings.Items[index];
                    ratedItems.Add(new RatedItem(itemId, userId, features));

                    //todo make linq extensions out of it
                    features.Where(d => !allFeatures
                    .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory))
                    .ToList()
                    .ForEach(f => allFeatures.Add(f));
                }


                //neural network
                
            }

            if (FeaturedRatings.Features.Count < 1)
                throw new ArgumentOutOfRangeException("Not enough features");

            
        }
    }
}
