namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationtablechanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "userEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "userEmail");
        }
    }
}
