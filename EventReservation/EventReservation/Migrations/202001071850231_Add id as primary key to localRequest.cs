namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddidasprimarykeytolocalRequest : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.LocalRequests");
            AddColumn("dbo.LocalRequests", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.LocalRequests", "Email", c => c.String());
            AddPrimaryKey("dbo.LocalRequests", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.LocalRequests");
            AlterColumn("dbo.LocalRequests", "Email", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.LocalRequests", "Id");
            AddPrimaryKey("dbo.LocalRequests", "Email");
        }
    }
}
