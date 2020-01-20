namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddateandtimetoeventmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TimeStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "TimeEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "Performer", c => c.String(nullable: false));
            DropColumn("dbo.Events", "DateEnd");
            DropColumn("dbo.Events", "BandName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "BandName", c => c.String());
            AddColumn("dbo.Events", "DateEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "Performer");
            DropColumn("dbo.Events", "TimeEnd");
            DropColumn("dbo.Events", "TimeStart");
        }
    }
}
