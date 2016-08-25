namespace Recommender.DataAccess.YahooMusic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenreId { get; set; }

        public int? ParentGenreId { get; set; }

        [ForeignKey("ParentGenreId")]
        public virtual Genre ParentGenre { get; set; }

        public int GenreLevel { get; set; }

        [Required]
        [StringLength(50)]
        public string GenreName { get; set; }
    }
}
