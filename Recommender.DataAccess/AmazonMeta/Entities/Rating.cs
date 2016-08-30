namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        [Column(TypeName = "date")]
        public DateTime ReviewDate { get; set; }

        [Key]
        [Column("ProductId", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Key]
        [Column("CustomerId", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }

        public int? ReviewRatingNumber { get; set; }

        public int? TotalVotesCount { get; set; }

        public int? HelpfulVotesCount { get; set; }
    }
}
