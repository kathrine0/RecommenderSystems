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
        [Column("ProductCode", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string ProductCode { get; set; }

        [ForeignKey("ProductCode")]
        public virtual Product Product { get; set; }

        [Key]
        [Column("CustomerCode", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string CustomerCode { get; set; }

        public int? ReviewRatingNumber { get; set; }

        public int? TotalVotesCount { get; set; }

        public int? HelpfulVotesCount { get; set; }
    }
}
