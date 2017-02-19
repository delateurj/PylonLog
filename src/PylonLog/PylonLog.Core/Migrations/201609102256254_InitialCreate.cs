namespace PylonLog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataBlocks",
                c => new
                    {
                        DataBlockID = c.Long(nullable: false, identity: true),
                        timeStamp = c.Int(nullable: false),
                        dataType = c.String(),
                        dataValue = c.Int(nullable: false),
                        PylonLogEntryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DataBlockID)
                .ForeignKey("dbo.PylonLogEntries", t => t.PylonLogEntryID, cascadeDelete: true)
                .Index(t => t.PylonLogEntryID);
            
            CreateTable(
                "dbo.PylonLogEntries",
                c => new
                    {
                        pylonLogEntryID = c.Int(nullable: false, identity: true),
                        entryDateTime = c.DateTime(nullable: false),
                        planeName = c.String(),
                        entryType = c.String(),
                        excludeFromStats = c.Boolean(nullable: false),
                        telemetryDuration = c.Int(nullable: false),
                        launchTimeStamp = c.Int(nullable: false),
                        endTimeStamp = c.Int(nullable: false),
                        peakRPMOnLine = c.Int(nullable: false),
                        launchRPM = c.Int(nullable: false),
                        avgRPM = c.Int(nullable: false),
                        avgPeakRPM = c.Int(nullable: false),
                        plugColor = c.Int(nullable: false),
                        relativeNeedle = c.String(),
                        newPlug = c.Boolean(nullable: false),
                        notes = c.String(),
                        temperature = c.Int(nullable: false),
                        humidity = c.Int(nullable: false),
                        headHeight = c.Int(nullable: false),
                        deckClearance = c.Int(nullable: false),
                        timing = c.String(),
                        engine_engineID = c.Int(),
                        plugType_GlowPlugID = c.Int(),
                        prop_PropID = c.Int(),
                    })
                .PrimaryKey(t => t.pylonLogEntryID)
                .ForeignKey("dbo.Engines", t => t.engine_engineID)
                .ForeignKey("dbo.GlowPlugs", t => t.plugType_GlowPlugID)
                .ForeignKey("dbo.Props", t => t.prop_PropID)
                .Index(t => t.engine_engineID)
                .Index(t => t.plugType_GlowPlugID)
                .Index(t => t.prop_PropID);
            
            CreateTable(
                "dbo.Engines",
                c => new
                    {
                        engineID = c.Int(nullable: false, identity: true),
                        serialNumber = c.String(),
                        engineType = c.String(),
                        notes = c.String(),
                    })
                .PrimaryKey(t => t.engineID);
            
            CreateTable(
                "dbo.GlowPlugs",
                c => new
                    {
                        GlowPlugID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.GlowPlugID);
            
            CreateTable(
                "dbo.Props",
                c => new
                    {
                        PropID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.PropID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PylonLogEntries", "prop_PropID", "dbo.Props");
            DropForeignKey("dbo.PylonLogEntries", "plugType_GlowPlugID", "dbo.GlowPlugs");
            DropForeignKey("dbo.PylonLogEntries", "engine_engineID", "dbo.Engines");
            DropForeignKey("dbo.DataBlocks", "PylonLogEntryID", "dbo.PylonLogEntries");
            DropIndex("dbo.PylonLogEntries", new[] { "prop_PropID" });
            DropIndex("dbo.PylonLogEntries", new[] { "plugType_GlowPlugID" });
            DropIndex("dbo.PylonLogEntries", new[] { "engine_engineID" });
            DropIndex("dbo.DataBlocks", new[] { "PylonLogEntryID" });
            DropTable("dbo.Props");
            DropTable("dbo.GlowPlugs");
            DropTable("dbo.Engines");
            DropTable("dbo.PylonLogEntries");
            DropTable("dbo.DataBlocks");
        }
    }
}
