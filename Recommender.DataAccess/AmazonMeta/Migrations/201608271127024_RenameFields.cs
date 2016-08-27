namespace Recommender.DataAccess.AmazonMeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFields : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Categories", "Product_ASIN_Code", "ProductCode");
            RenameColumn("dbo.Categories", "Category_Code", "CategoryCode");
            RenameColumn("dbo.Categories", "Category_Name", "CategoryName");
            RenameColumn("dbo.Products", "Product_ASIN_Code", "ProductCode");
            RenameColumn("dbo.Products", "Product_Title_Text", "ProductTitle");
            RenameColumn("dbo.Products", "Product_Group_Description", "ProductGroupDescription");
            RenameColumn("dbo.Products", "Sales_Rank_Number", "SalesRankNumber");
            RenameColumn("dbo.Products", "Copurchased_Products_Count", "CopurchasedProductsCount");
            RenameColumn("dbo.Products", "Category_Hierarchy_Count", "CategoryHierarchyCount");
            RenameColumn("dbo.Products", "Reviews_Count", "ReviewsCount");
            RenameColumn("dbo.Products", "Downloaded_Reviews_Count","DownloadedReviewCount");
            RenameColumn("dbo.Products", "Average_Review_Rating_Number", "AverageReviewRatingNumber");
            RenameColumn("dbo.Ratings", "Review_Date", "ReviewDate");
            RenameColumn("dbo.Ratings", "Product_ASIN_Code", "ProductCode");
            RenameColumn("dbo.Ratings", "Customer_Code","CustomerCode");
            RenameColumn("dbo.Ratings", "Review_Rating_Number", "ReviewRatingNumber");
            RenameColumn("dbo.Ratings", "Total_Votes_Count", "TotalVotesCount");
            RenameColumn("dbo.Ratings", "Helpful_Votes_Count", "HelpfulVotesCount");

        }
        
        public override void Down()
        {
            RenameColumn("dbo.Categories", "ProductCode", "Product_ASIN_Code");
            RenameColumn("dbo.Categories", "CategoryCode", "Category_Code");
            RenameColumn("dbo.Categories", "CategoryName", "Category_Name");
            RenameColumn("dbo.Products", "ProductCode", "Product_ASIN_Code");
            RenameColumn("dbo.Products", "ProductTitle", "Product_Title_Text");
            RenameColumn("dbo.Products", "ProductGroupDescription", "Product_Group_Description");
            RenameColumn("dbo.Products", "SalesRankNumber", "Sales_Rank_Number");
            RenameColumn("dbo.Products", "CopurchasedProductsCount", "Copurchased_Products_Count");
            RenameColumn("dbo.Products", "CategoryHierarchyCount", "Category_Hierarchy_Count");
            RenameColumn("dbo.Products", "ReviewsCount", "Reviews_Count");
            RenameColumn("dbo.Products", "DownloadedReviewCount", "Downloaded_Reviews_Count");
            RenameColumn("dbo.Products", "AverageReviewRatingNumber", "Average_Review_Rating_Number");
            RenameColumn("dbo.Ratings", "ReviewDate", "Review_Date");
            RenameColumn("dbo.Ratings", "ProductCode", "Product_ASIN_Code");
            RenameColumn("dbo.Ratings", "CustomerCode", "Customer_Code");
            RenameColumn("dbo.Ratings", "ReviewRatingNumber", "Review_Rating_Number");
            RenameColumn("dbo.Ratings", "TotalVotesCount", "Total_Votes_Count");
            RenameColumn("dbo.Ratings", "HelpfulVotesCount", "Helpful_Votes_Count");
        }
    }
}
