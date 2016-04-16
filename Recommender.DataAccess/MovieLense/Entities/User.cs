using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommender.DataAccess.MovieLense.Entities
{
    public partial class User
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; }

        public int Age { get; set; }

        public int Occupation { get; set; }

        [Required]
        [StringLength(10)]
        public string Zipcode { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

    }
}
