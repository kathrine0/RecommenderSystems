namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string ProductCode { get; set; }

        [StringLength(1000)]
        public string ProductTitle { get; set; }

        [StringLength(50)]
        public string ProductGroupDescription { get; set; }

        public int? SalesRankNumber { get; set; }

        public int? CopurchasedProductsCount { get; set; }

        public int? CategoryHierarchyCount { get; set; }

        public int? ReviewsCount { get; set; }

        public int? DownloadedReviewCount { get; set; }

        public decimal? AverageReviewRatingNumber { get; set; }
    }
}
