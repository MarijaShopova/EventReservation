namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reguiredFieldChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "BandName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "BandName", c => c.String());
            AlterColumn("dbo.Events", "Description", c => c.String());
        }
    }
}
