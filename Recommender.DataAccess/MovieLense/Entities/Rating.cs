using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommender.DataAccess.MovieLense.Entities
{

    public partial class Rating
    {
        [Key]
        [Column("user_id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? UserId { get; set; }

        public virtual User User { get; set; }

        [Key]
        [Column("movie_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Column("rating")]
        public float TheRating { get; set; }

        public int Timestamp { get; set; }
    }
}
