namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEvenetprefixfromEventTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Events", "Info", c => c.String());
            AddColumn("dbo.Events", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "EventName");
            DropColumn("dbo.Events", "EventInfo");
            DropColumn("dbo.Events", "EventStartDate");
            DropColumn("dbo.Events", "EventEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EventStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EventInfo", c => c.String());
            AddColumn("dbo.Events", "EventName", c => c.String(nullable: false));
            DropColumn("dbo.Events", "EndDate");
            DropColumn("dbo.Events", "StartDate");
            DropColumn("dbo.Events", "Info");
            DropColumn("dbo.Events", "Name");
        }
    }
}
