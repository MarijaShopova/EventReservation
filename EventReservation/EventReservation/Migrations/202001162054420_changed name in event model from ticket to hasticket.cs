namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changednameineventmodelfromtickettohasticket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "HasTicket", c => c.Boolean(nullable: false));
            DropColumn("dbo.Events", "Ticket");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Ticket", c => c.Boolean(nullable: false));
            DropColumn("dbo.Events", "HasTicket");
        }
    }
}
