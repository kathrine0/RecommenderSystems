namespace Recommender.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6updateschema : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Ratings", "user_id");
            CreateIndex("dbo.Ratings", "movie_id");
            AddForeignKey("dbo.Ratings", "movie_id", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "user_id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "user_id", "dbo.Users");
            DropForeignKey("dbo.Ratings", "movie_id", "dbo.Movies");
            DropIndex("dbo.Ratings", new[] { "movie_id" });
            DropIndex("dbo.Ratings", new[] { "user_id" });
        }
    }
}
