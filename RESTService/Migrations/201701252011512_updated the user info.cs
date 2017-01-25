namespace RESTService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedtheuserinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "GivenName", c => c.String());
            AddColumn("dbo.Users", "FamilyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FamilyName");
            DropColumn("dbo.Users", "GivenName");
        }
    }
}
