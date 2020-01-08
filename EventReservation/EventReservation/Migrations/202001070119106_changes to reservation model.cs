namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestoreservationmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        NoTables = c.Int(nullable: false),
                        eventId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Events", t => t.eventId_Id)
                .Index(t => t.eventId_Id);
            
            CreateTable(
                "dbo.LocalRequests",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        LocalName = c.String(),
                        LocalCity = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
            AddColumn("dbo.Events", "NoTables", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "ReservedTables", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "BandName", c => c.String());
            DropColumn("dbo.Events", "FreeTables");
            DropColumn("dbo.Locals", "NoTables");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locals", "NoTables", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "FreeTables", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reservations", "eventId_Id", "dbo.Events");
            DropIndex("dbo.Reservations", new[] { "eventId_Id" });
            AlterColumn("dbo.Events", "BandName", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Events", "ReservedTables");
            DropColumn("dbo.Events", "NoTables");
            DropTable("dbo.LocalRequests");
            DropTable("dbo.Reservations");
        }
    }
}
