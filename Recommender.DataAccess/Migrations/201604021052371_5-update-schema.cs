namespace Recommender.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5updateschema : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovyActors", newName: "MovieActors");
            RenameColumn(table: "dbo.MovieActors", name: "Movy_Id", newName: "Movie_Id");
            RenameIndex(table: "dbo.MovieActors", name: "IX_Movy_Id", newName: "IX_Movie_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MovieActors", name: "IX_Movie_Id", newName: "IX_Movy_Id");
            RenameColumn(table: "dbo.MovieActors", name: "Movie_Id", newName: "Movy_Id");
            RenameTable(name: "dbo.MovieActors", newName: "MovyActors");
        }
    }
}
