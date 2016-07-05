using System;
using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class RatedItem
    {
        public RatedItem(float rating, IList<IFeature> features) 
        {
            Rating = rating;
            Features = features;
        }

        public IList<IFeature> Features { get; set; }

        public float Rating { get; set; }
    }
}