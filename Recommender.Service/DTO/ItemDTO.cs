﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Service.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }

        public IEnumerable<string> ItemFeatures { get; set; }

        //todo think about complex features

        public virtual ICollection<RatingDTO> Ratings { get; set; }
    }
}
