namespace Recommender.DataAccess.MovieLense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10updateschema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.Movies", "country_id", "dbo.Countries");
            DropForeignKey("dbo.Movies", "director_id", "dbo.Directors");
            DropForeignKey("dbo.Movies", "language_id", "dbo.Languages");
            DropIndex("dbo.Movies", new[] { "director_id" });
            DropIndex("dbo.Movies", new[] { "language_id" });
            DropIndex("dbo.Movies", new[] { "country_id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            AddColumn("dbo.Movies", "Director", c => c.String());
            AddColumn("dbo.Movies", "Actors", c => c.String());
            AddColumn("dbo.Movies", "Language", c => c.String());
            AddColumn("dbo.Movies", "Country", c => c.String());
            DropColumn("dbo.Movies", "director_id");
            DropColumn("dbo.Movies", "language_id");
            DropColumn("dbo.Movies", "country_id");
            DropTable("dbo.Actors");
            DropTable("dbo.Countries");
            DropTable("dbo.Directors");
            DropTable("dbo.Languages");
            DropTable("dbo.Genres");
            DropTable("dbo.MovieActors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id });
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        genre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "country_id", c => c.Int());
            AddColumn("dbo.Movies", "language_id", c => c.Int());
            AddColumn("dbo.Movies", "director_id", c => c.Int());
            DropColumn("dbo.Movies", "Country");
            DropColumn("dbo.Movies", "Language");
            DropColumn("dbo.Movies", "Actors");
            DropColumn("dbo.Movies", "Director");
            CreateIndex("dbo.MovieActors", "Actor_Id");
            CreateIndex("dbo.MovieActors", "Movie_Id");
            CreateIndex("dbo.Movies", "country_id");
            CreateIndex("dbo.Movies", "language_id");
            CreateIndex("dbo.Movies", "director_id");
            AddForeignKey("dbo.Movies", "language_id", "dbo.Languages", "Id");
            AddForeignKey("dbo.Movies", "director_id", "dbo.Directors", "Id");
            AddForeignKey("dbo.Movies", "country_id", "dbo.Countries", "Id");
            AddForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
