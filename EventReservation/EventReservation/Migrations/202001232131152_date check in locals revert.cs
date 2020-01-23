namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datecheckinlocalsrevert : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locals", "OpeningHour", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Locals", "ClosingHour", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locals", "ClosingHour", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Locals", "OpeningHour", c => c.DateTime(nullable: false));
        }
    }
}
