using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class old_UserProfile
    {
        public old_UserProfile(int userId)
        {
            UserId = userId;
            RatedFeatures = new List<old_Feature>();
        }

        public int UserId { get; set; }

        public List<old_Feature> RatedFeatures { get; set; }
    }
}
