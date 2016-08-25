namespace Recommender.DataAccess.YahooMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreFieldNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Genres", "genre_id", "GenreId");
            RenameColumn("dbo.Genres", "parent_genre_id", "ParentGenreId");
            RenameColumn("dbo.Genres", "genre_level", "GenreLevel");
            RenameColumn("dbo.Genres", "genre_name", "GenreName");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Genres", "GenreId", "genre_id");
            RenameColumn("dbo.Genres", "ParentGenreId", "parent_genre_id");
            RenameColumn("dbo.Genres", "GenreLevel", "genre_level");
            RenameColumn("dbo.Genres", "GenreName", "genre_name");
        }
    }
}
