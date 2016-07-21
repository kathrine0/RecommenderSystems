namespace Recommender.DataAccess.YahooMusic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SongAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int song_id { get; set; }

        public int album_id { get; set; }

        public int artist_id { get; set; }

        public int genre_id { get; set; }
    }
}
