namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {

        [Key]
        [Column("ProductCode", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string ProductCode { get; set; }

        [ForeignKey("ProductCode")]
        public virtual Product Product { get; set; }

        [Key]
        [Column("CategoryCode", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string CategoryCode { get; set; }

        [StringLength(1000)]
        public string CategoryName { get; set; }
    }
}
