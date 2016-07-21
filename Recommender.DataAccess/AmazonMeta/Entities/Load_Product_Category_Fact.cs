namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Load_Product_Category_Fact
    {
        [Key]
        public int Load_Product_Category_Fact_Key { get; set; }

        [StringLength(20)]
        public string Product_ASIN_Code { get; set; }

        [StringLength(20)]
        public string Category_Code { get; set; }

        [StringLength(1000)]
        public string Category_Name { get; set; }
    }
}
