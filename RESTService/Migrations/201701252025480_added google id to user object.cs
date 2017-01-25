namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgoogleidtouserobject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "GoogleId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "GoogleId");
        }
    }
}
