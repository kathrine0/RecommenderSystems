namespace Recommender.DataAccess.AmazonMeta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changenames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Load_Product_Category_Fact", newName: "Categories");
            RenameTable(name: "dbo.Load_Product_Dimension", newName: "Products");
            RenameTable(name: "dbo.Load_Review_Fact", newName: "Ratings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Ratings", newName: "Load_Review_Fact");
            RenameTable(name: "dbo.Products", newName: "Load_Product_Dimension");
            RenameTable(name: "dbo.Categories", newName: "Load_Product_Category_Fact");
        }
    }
}
