namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localsModelEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locals", "LocalsImage", c => c.Binary(storeType: "image"));
            AddColumn("dbo.LocalRequests", "LocalsImage", c => c.Binary(storeType: "image"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocalRequests", "LocalsImage");
            DropColumn("dbo.Locals", "LocalsImage");
        }
    }
}
