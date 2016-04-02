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
            Actors = new HashSet<Actor>();
            Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Genres { get; set; }

        [Column("director_id")]
        public int? DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public int? Year { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
