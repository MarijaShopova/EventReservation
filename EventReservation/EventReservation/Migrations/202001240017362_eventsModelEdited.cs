namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventsModelEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventImage", c => c.Binary(storeType: "image"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EventImage");
        }
    }
}
