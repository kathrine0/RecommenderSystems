using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommender.DataAccess.Entities
{

    public partial class Link
    {
        [Key]
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Required]
        [StringLength(50)]
        public string ImdbId { get; set; }

        [StringLength(50)]
        public string TmdbId { get; set; }
    }
}
