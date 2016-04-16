namespace Recommender.DataAccess.MovieLense.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9updateschema : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "MovieId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Links", "MovieId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Links");
            AlterColumn("dbo.Links", "MovieId", c => c.Int(nullable: false, identity: true));
        }
    }
}
