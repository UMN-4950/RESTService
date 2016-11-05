namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserEventmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventInfo = c.String(),
                        EventStartDate = c.DateTime(nullable: false),
                        EventEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        User_Id = c.Int(),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Event_Id);
            
            AddColumn("dbo.Locations", "User_Id", c => c.Int());
            AddColumn("dbo.Locations", "Event_Id", c => c.Int());
            CreateIndex("dbo.Locations", "User_Id");
            CreateIndex("dbo.Locations", "Event_Id");
            AddForeignKey("dbo.Locations", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Locations", "Event_Id", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Users", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Users", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Locations", "User_Id", "dbo.Users");
            DropIndex("dbo.Locations", new[] { "Event_Id" });
            DropIndex("dbo.Locations", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Event_Id" });
            DropIndex("dbo.Users", new[] { "User_Id" });
            DropColumn("dbo.Locations", "Event_Id");
            DropColumn("dbo.Locations", "User_Id");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
        }
    }
}
