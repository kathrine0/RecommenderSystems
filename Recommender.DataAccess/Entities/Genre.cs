using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommender.DataAccess.Entities
{

    public partial class Genre
    {
        public int Id { get; set; }

        [Column("genre")]
        [Required]
        [StringLength(50)]
        public string TheGenre { get; set; }
    }
}
