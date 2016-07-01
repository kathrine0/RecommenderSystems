using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class old_Feature
    {
        public old_Feature(string name)
        {
            Name = name;
            Rating = new List<old_FeatureRating>();
        }

        public string Name { get; set; }

        /// <summary>
        /// Dictionary containing pairs: item and average rating by user
        /// </summary>
        public List<old_FeatureRating> Rating { get; set; }
    }

    public class old_FeatureRating
    {
        public old_FeatureRating(string itemName, double averageRating)
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