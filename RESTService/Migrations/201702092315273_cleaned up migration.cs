namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanedupmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Info = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Users", t => t.LocationId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        GoogleId = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        GivenName = c.String(),
                        FamilyName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Message = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "LocationId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Locations", new[] { "LocationId" });
            DropTable("dbo.Notifications");
            DropTable("dbo.Users");
            DropTable("dbo.Locations");
            DropTable("dbo.Events");
        }
    }
}
