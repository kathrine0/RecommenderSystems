using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Service.DTO
{
    public class RatingWithFeaturesDTO
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public float Rating { get; set; }
        public IEnumerable<string> ItemFeatures { get; set; }
    }
}
