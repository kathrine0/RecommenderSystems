namespace Recommender.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7updateschema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "Movie_Id", c => c.Int());
            CreateIndex("dbo.Links", "Movie_Id");
            AddForeignKey("dbo.Links", "Movie_Id", "dbo.Movies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Links", new[] { "Movie_Id" });
            DropColumn("dbo.Links", "Movie_Id");
        }
    }
}
