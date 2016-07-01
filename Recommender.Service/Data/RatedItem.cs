using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class RatedItem
    {
        public RatedItem(int itemId, int userId, float rating, IList<IFeature> features) : this(itemId, userId, rating)
        {
            Features = features;
        }

        public RatedItem(int itemId, int userId, float rating)
        {
            ItemId = itemId;
            UserId = userId;
            Rating = rating;
            Features = new List<IFeature>();
        }

        public float Rating { get; set; }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public IList<IFeature> Features { get; set; }
    }
}