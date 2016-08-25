namespace Recommender.DataAccess.YahooMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SongFieldNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Songs", "song_id", "SongId");
            RenameColumn("dbo.Songs", "album_id", "AlbumId");
            RenameColumn("dbo.Songs", "artist_id", "ArtistId");
            RenameColumn("dbo.Songs", "genre_id", "GenreId");
    }
        
        public override void Down()
        {
            RenameColumn("dbo.Songs", "SongId", "song_id");
            RenameColumn("dbo.Songs", "AlbumId", "album_id");
            RenameColumn("dbo.Songs", "ArtistId", "artist_id");
            RenameColumn("dbo.Songs", "GenreId", "genre_id");
        }
    }
}
