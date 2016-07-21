namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Load_Product_Dimension
    {
        [Key]
        public int Load_Product_Dimension_Key { get; set; }

        [StringLength(20)]
        public string Product_ASIN_Code { get; set; }

        [StringLength(1000)]
        public string Product_Title_Text { get; set; }

        [StringLength(50)]
        public string Product_Group_Description { get; set; }

        public int? Sales_Rank_Number { get; set; }

        public int? Copurchased_Products_Count { get; set; }

        public int? Category_Hierarchy_Count { get; set; }

        public int? Reviews_Count { get; set; }

        public int? Downloaded_Reviews_Count { get; set; }

        public decimal? Average_Review_Rating_Number { get; set; }
    }
}
