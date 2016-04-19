using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class Feature
    {
        public Feature(string name)
        {
            Name = name;
            Rating = new List<FeatureRating>();
        }

        public string Name { get; set; }

        /// <summary>
        /// Dictionary containing pairs: item and average rating by user
        /// </summary>
        public List<FeatureRating> Rating { get; set; }
    }

    public class FeatureRating
    {
        public FeatureRating(string itemName, double averageRating)
        {
            ItemName = itemName;
            AverageRating = averageRating;
            _timesRated = 1;
        }

        public string ItemName { get; set; }

        public double AverageRating { get; set; }

        int _timesRated;

        public void AddRating(double rating)
        {
            AverageRating = (AverageRating * _timesRated + rating) / (_timesRated + 1);
            _timesRated++;
        }
    }
}