namespace Recommender.DataAccess.MovieLense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateschema : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Directors", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Genres", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Links", "MovieId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Movies", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "id", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "id", c => c.Int(nullable: false));
            AlterColumn("dbo.Links", "MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Genres", "id", c => c.Int(nullable: false));
            AlterColumn("dbo.Directors", "id", c => c.Int(nullable: false));
            AlterColumn("dbo.Actors", "id", c => c.Int(nullable: false));
        }
    }
}
