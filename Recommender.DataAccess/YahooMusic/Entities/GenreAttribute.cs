namespace Recommender.DataAccess.YahooMusic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GenreAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int genre_id { get; set; }

        public int? parent_genre_id { get; set; }

        public int genre_level { get; set; }

        [Required]
        [StringLength(50)]
        public string genre_name { get; set; }
    }
}
