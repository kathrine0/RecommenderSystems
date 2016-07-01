using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public class RatedItem
    {
        public RatedItem(int itemId, int userId, IList<IFeature> features) : this(itemId, userId)
        {
            Features = features;
        }

        public RatedItem(int itemId, int userId)
        {
            ItemId = itemId;
            UserId = userId;
            Features = new List<IFeature>();
        }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public IList<IFeature> Features { get; set; }
    }
}