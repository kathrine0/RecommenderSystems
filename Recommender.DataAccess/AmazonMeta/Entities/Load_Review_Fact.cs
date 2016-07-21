namespace Recommender.DataAccess.AmazonMeta.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Load_Review_Fact
    {
        [Key]
        public int Load_Review_Fact_Key { get; set; }

        [Column(TypeName = "date")]
        public DateTime Review_Date { get; set; }

        [StringLength(20)]
        public string Product_ASIN_Code { get; set; }

        [StringLength(20)]
        public string Customer_Code { get; set; }

        public int? Review_Rating_Number { get; set; }

        public int? Total_Votes_Count { get; set; }

        public int? Helpful_Votes_Count { get; set; }
    }
}
