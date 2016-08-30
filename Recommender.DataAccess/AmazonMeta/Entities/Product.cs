namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public Product()
        {
            Categories = new HashSet<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [StringLength(1000)]
        public string ProductTitle { get; set; }

        [StringLength(50)]
        public string ProductGroupDescription { get; set; }

        public int? SalesRankNumber { get; set; }

        public int? CopurchasedProductsCount { get; set; }

        public int? CategoryHierarchyCount { get; set; }

        public int? ReviewsCount { get; set; }

        public int? DownloadedReviewsCount { get; set; }

        public decimal? AverageReviewRatingNumber { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
