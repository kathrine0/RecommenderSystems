namespace Recommender.DataAccess.YahooMusic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedNames2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreAttributes", newName: "Genres");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Genres", newName: "GenreAttributes");
        }
    }
}
