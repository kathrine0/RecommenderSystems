namespace Recommender.DataAccess.YahooMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ocochodzi : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Genres", "ParentGenreId");
            AddForeignKey("dbo.Genres", "ParentGenreId", "dbo.Genres", "GenreId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genres", "ParentGenreId", "dbo.Genres");
            DropIndex("dbo.Genres", new[] { "ParentGenreId" });
        }
    }
}
