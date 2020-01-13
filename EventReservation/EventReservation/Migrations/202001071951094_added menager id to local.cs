namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmenageridtolocal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locals", "Manager", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locals", "Manager");
        }
    }
}
