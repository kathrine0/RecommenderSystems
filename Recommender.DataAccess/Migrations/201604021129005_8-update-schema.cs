namespace Recommender.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8updateschema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            
            AddColumn("dbo.Movies", "language_id", c => c.Int());
            AddColumn("dbo.Movies", "country_id", c => c.Int());
            AddColumn("dbo.Movies", "ImdbRating", c => c.Double());
            CreateIndex("dbo.Movies", "language_id");
            CreateIndex("dbo.Movies", "country_id");
            AddForeignKey("dbo.Movies", "country_id", "dbo.Countries", "Id");
            AddForeignKey("dbo.Movies", "language_id", "dbo.Languages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "language_id", "dbo.Languages");
            DropForeignKey("dbo.Movies", "country_id", "dbo.Countries");
            DropIndex("dbo.Movies", new[] { "country_id" });
            DropIndex("dbo.Movies", new[] { "language_id" });
            DropColumn("dbo.Movies", "ImdbRating");
            DropColumn("dbo.Movies", "country_id");
            DropColumn("dbo.Movies", "language_id");
            DropTable("dbo.Languages");
            DropTable("dbo.Countries");
        }
    }
}
