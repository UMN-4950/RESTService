namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locationlist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "LocationId", "dbo.Users");
            DropIndex("dbo.Locations", new[] { "LocationId" });
            DropPrimaryKey("dbo.Locations");
            AddColumn("dbo.Locations", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Locations", "LocationId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Locations", "LocationId");
            CreateIndex("dbo.Locations", "UserId");
            AddForeignKey("dbo.Locations", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "UserId", "dbo.Users");
            DropIndex("dbo.Locations", new[] { "UserId" });
            DropPrimaryKey("dbo.Locations");
            AlterColumn("dbo.Locations", "LocationId", c => c.Int(nullable: false));
            DropColumn("dbo.Locations", "UserId");
            AddPrimaryKey("dbo.Locations", "LocationId");
            CreateIndex("dbo.Locations", "LocationId");
            AddForeignKey("dbo.Locations", "LocationId", "dbo.Users", "UserId");
        }
    }
}
