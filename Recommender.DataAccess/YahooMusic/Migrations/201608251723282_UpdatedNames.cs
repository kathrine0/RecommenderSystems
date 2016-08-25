namespace Recommender.DataAccess.YahooMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SongAttributes", newName: "Songs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Songs", newName: "SongAttributes");
        }
    }
}
