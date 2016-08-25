namespace Recommender.DataAccess.YahooMusic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        [Key]
        [Column("user_id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column("song_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; }

        [Column("rating")]
        public int TheRating { get; set; }
    }
}
