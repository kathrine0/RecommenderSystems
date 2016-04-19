using Recommender.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Core.MachineLearning
{
    public class UserProfileBuilder
    {
        
        public IList<UserProfile> Build(IFeaturedRatings featured_ratings, IList<int> rating_indices)
        {
            var userProfiles = new List<UserProfile>();

            foreach (int index in rating_indices)
            {
                //skip unrated elements
                if (Single.IsNaN(featured_ratings[index]))
                    continue;

                //skip elements with no features
                if (featured_ratings.Features[index].Any(x => String.IsNullOrEmpty(x.Value)))
                    continue;

                int userId = featured_ratings.Users[index];
                var rating = featured_ratings[index];

                //todo write some nice linq extension here
                var userProfile = userProfiles.FirstOrDefault(x => x.UserId == userId);

                if (userProfile == null)
                {
                    userProfile = new UserProfile(userId);
                    userProfiles.Add(userProfile);
                }

                var features = featured_ratings.Features[index];
                PopulateFeaturesCategories(userProfile, features, rating);
            }

            return userProfiles;
        }

        private void PopulateFeaturesCategories(UserProfile userProfile, IDictionary<string,string> features, double rating)
        {
            foreach (var feature in features)
            {
                //Add new category of features to user profile (ie actors or producers)
                var category = userProfile.RatedFeatures.FirstOrDefault(x => x.Name == feature.Key);

                if (category == null)
                {
                    category = new Feature(feature.Key);
                    userProfile.RatedFeatures.Add(category);
                }

                PopulateCategories(category, feature, rating);
            }
        }

        private void PopulateCategories(Feature category, KeyValuePair<string, string> feature, double rating)
        {
            //populate category with items (ie Pierce Brosnan)
            //first split complex entries
            var itemsName = feature.Value.Split(Settings.Delimeters, StringSplitOptions.RemoveEmptyEntries);

            foreach (var itemName in itemsName)
            {
                var itemRating = category.Rating.FirstOrDefault(x => x.ItemName == itemName);

                //such element has not been rated yet. Add. EOT.
                if (itemRating == null)
                {
                    category.Rating.Add(new FeatureRating(itemName, rating));
                }
                else //else update rating
                {
                    itemRating.AddRating(rating);
                }
            }
        }
    }
}
