namespace Recommender.DataAccess.MovieLense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4updateschema : DbMigration
    {
        public override void Up()
        {
            AddPrimaryKey("dbo.Ratings", new[] { "user_id", "movie_id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Ratings");
        }
    }
}
