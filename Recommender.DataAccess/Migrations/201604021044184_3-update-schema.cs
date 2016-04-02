namespace Recommender.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3updateschema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovyActors",
                c => new
                    {
                        Movy_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movy_Id, t.Actor_Id })
                .ForeignKey("dbo.Movies", t => t.Movy_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movy_Id)
                .Index(t => t.Actor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovyActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovyActors", "Movy_Id", "dbo.Movies");
            DropIndex("dbo.MovyActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovyActors", new[] { "Movy_Id" });
            DropTable("dbo.MovyActors");
        }
    }
}
