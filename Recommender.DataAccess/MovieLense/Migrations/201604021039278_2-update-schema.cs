namespace Recommender.DataAccess.MovieLense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2updateschema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "director_id", c => c.Int());
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Directors", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Genres", "genre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Links", "ImdbId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Links", "TmdbId", c => c.String(maxLength: 50));
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Movies", "Genres", c => c.String(maxLength: 255));
            AlterColumn("dbo.Users", "Gender", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            CreateIndex("dbo.Movies", "director_id");
            AddForeignKey("dbo.Movies", "director_id", "dbo.Directors", "Id");
            DropColumn("dbo.Movies", "Director");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Director", c => c.Int());
            DropForeignKey("dbo.Movies", "director_id", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "director_id" });
            AlterColumn("dbo.Users", "Gender", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.Movies", "Genres", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Links", "TmdbId", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Links", "ImdbId", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Genres", "genre", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Directors", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
            DropColumn("dbo.Movies", "director_id");
        }
    }
}
