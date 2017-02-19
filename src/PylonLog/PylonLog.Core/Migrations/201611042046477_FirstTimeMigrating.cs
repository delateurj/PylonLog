namespace PylonLog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstTimeMigrating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PylonLogEntries", "engine_engineID", "dbo.Engines");
            DropIndex("dbo.PylonLogEntries", new[] { "engine_engineID" });
            AddColumn("dbo.PylonLogEntries", "engine", c => c.String());
            AddColumn("dbo.Engines", "headHeight", c => c.Int(nullable: false));
            AddColumn("dbo.Engines", "deckClearance", c => c.Int(nullable: false));
            AddColumn("dbo.Engines", "timing", c => c.String());
            DropColumn("dbo.PylonLogEntries", "engine_engineID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PylonLogEntries", "engine_engineID", c => c.Int());
            DropColumn("dbo.Engines", "timing");
            DropColumn("dbo.Engines", "deckClearance");
            DropColumn("dbo.Engines", "headHeight");
            DropColumn("dbo.PylonLogEntries", "engine");
            CreateIndex("dbo.PylonLogEntries", "engine_engineID");
            AddForeignKey("dbo.PylonLogEntries", "engine_engineID", "dbo.Engines", "engineID");
        }
    }
}
