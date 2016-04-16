using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recommender.DataAccess.MovieLense.Entities
{

    public partial class Link
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }

        [Required]
        [StringLength(50)]
        public string ImdbId { get; set; }

        [StringLength(50)]
        public string TmdbId { get; set; }
    }
}
