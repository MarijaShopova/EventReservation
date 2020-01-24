namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bla : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Genre", c => c.String(nullable: false));
            DropColumn("dbo.Events", "EventImage");
            DropColumn("dbo.Locals", "LocalsImage");
            DropColumn("dbo.LocalRequests", "LocalsImages");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LocalRequests", "LocalsImages", c => c.Binary());
            AddColumn("dbo.Locals", "LocalsImage", c => c.Binary(storeType: "image"));
            AddColumn("dbo.Events", "EventImage", c => c.Binary(nullable: false));
            AlterColumn("dbo.Events", "Genre", c => c.String());
        }
    }
}
