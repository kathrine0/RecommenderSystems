using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class UserProfile
    {
        public UserProfile(int userId)
        {
            UserId = userId;
            RatedMovies = new List<RatedItem>();
            Features = new List<Feature>();
        }

        public int UserId { get; set; }

        public IList<RatedItem> RatedMovies { get; set; }
        public IList<Feature> Features { get; set; }
    }
}
