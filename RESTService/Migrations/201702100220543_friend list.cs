namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class friendlist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Locations", "LocationId", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "UserId");
            AddForeignKey("dbo.Locations", "LocationId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Locations", "LocationId", "dbo.Users");
            DropForeignKey("dbo.Friends", "UserId", "dbo.Users");
            DropIndex("dbo.Friends", new[] { "UserId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false));
            DropTable("dbo.Friends");
            AddPrimaryKey("dbo.Users", "UserId");
            AddForeignKey("dbo.Locations", "LocationId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
