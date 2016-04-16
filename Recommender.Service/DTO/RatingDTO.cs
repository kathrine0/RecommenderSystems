using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Service.DTO
{
    public class RatingDTO
    {
        public int? UserId { get; set; }
        public int? MovieId { get; set; }
        public float TheRating { get; set; }
    }
}
