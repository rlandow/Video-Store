namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipTypeAddName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false));
            Sql("Update dbo.MembershipTypes   Set name = 'Pay as you go' where id = 1" +
                "Update dbo.MembershipTypes Set name = 'Monthly' where id = 2" +
                "Update dbo.MembershipTypes Set name = 'Quarterly' where id = 3" +
                "Update dbo.MembershipTypes Set name = 'Annual' where id = 4" 

                );
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
