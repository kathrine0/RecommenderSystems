using System;
using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class RatedFeature : Feature
    {
        public RatedFeature(Feature feature, double rating) : base(feature.Name, feature.FeatureCategory, feature.Id)
        {
            AverageRating = rating;
            _timesRated = 1;
        }

        //public RatedFeature(string name, string featureCategory, double rating) : base(name, featureCategory)
        //{
        //    AverageRating = rating;
        //    _timesRated = 1;
        //}

        int _timesRated;

        public double AverageRating { get; set; }

        public void AddRating(double rating)
        {
            AverageRating = (AverageRating * _timesRated + rating) / (_timesRated + 1);
            _timesRated++;
        }
    }

    public class Feature : IFeature
    {
        public Feature(string name, string featureCategory, string id)
        {
            Id = id;
            Name = name;
            FeatureCategory = featureCategory;
        }

        public Feature(string name, string featureCategory)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            FeatureCategory = featureCategory;
        }

        //todo change accessibility of setter
        public string Id { get; set; }

        public string Name { get; set; }

        public string FeatureCategory { get; set; }
    }
}