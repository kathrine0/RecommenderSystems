using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class UserProfile
    {
        public UserProfile(int userId)
        {
            UserId = userId;
            RatedFeatures = new List<Feature>();
        }

        public int UserId { get; set; }

        public List<Feature> RatedFeatures { get; set; }
    }
}
