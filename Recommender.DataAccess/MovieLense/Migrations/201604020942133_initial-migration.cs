namespace Recommender.DataAccess.MovieLense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        id = c.Int(nullable: false),
                        name = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        id = c.Int(nullable: false),
                        genre = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        movieId = c.Int(nullable: false),
                        imdbId = c.String(nullable: false, maxLength: 50, unicode: false),
                        tmdbId = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.movieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        id = c.Int(nullable: false),
                        title = c.String(nullable: false, maxLength: 255, unicode: false),
                        genres = c.String(maxLength: 255, unicode: false),
                        director = c.Int(),
                        year = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        user_id = c.Int(nullable: false),
                        movie_id = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        timestamp = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.user_id, t.movie_id, t.rating, t.timestamp });
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false),
                        gender = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        age = c.Int(nullable: false),
                        occupation = c.Int(nullable: false),
                        zipcode = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Ratings");
            DropTable("dbo.Movies");
            DropTable("dbo.Links");
            DropTable("dbo.Genres");
            DropTable("dbo.Directors");
            DropTable("dbo.Actors");
        }
    }
}
