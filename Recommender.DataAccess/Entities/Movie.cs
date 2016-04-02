using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommender.DataAccess.Entities
{

    [Table("Movies")]
    public partial class Movie
    {
        public Movie()
        {
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Genres { get; set; }

        public string Director { get; set; }

        public int? Year { get; set; }

        public string Actors { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public string Language {get; set; }

        public string Country { get; set; }

        public double? ImdbRating { get; set; }
    }
}
