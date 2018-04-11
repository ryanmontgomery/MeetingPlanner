namespace MeetingPlanner.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bishoprics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Calling = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Description = c.String(maxLength: 100),
                        Order = c.Int(nullable: false),
                        MeetingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Meetings", t => t.MeetingID, cascadeDelete: true)
                .Index(t => t.MeetingID);
            
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MeetingDate = c.DateTime(nullable: false),
                        BishopricID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Bishoprics", t => t.BishopricID, cascadeDelete: true)
                .Index(t => t.BishopricID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "MeetingID", "dbo.Meetings");
            DropForeignKey("dbo.Meetings", "BishopricID", "dbo.Bishoprics");
            DropIndex("dbo.Meetings", new[] { "BishopricID" });
            DropIndex("dbo.Events", new[] { "MeetingID" });
            DropTable("dbo.Meetings");
            DropTable("dbo.Events");
            DropTable("dbo.Bishoprics");
        }
    }
}
