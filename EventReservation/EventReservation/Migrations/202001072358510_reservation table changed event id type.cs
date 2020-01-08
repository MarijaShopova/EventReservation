namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationtablechangedeventidtype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "eventId_Id", "dbo.Events");
            DropIndex("dbo.Reservations", new[] { "eventId_Id" });
            RenameColumn(table: "dbo.Reservations", name: "eventId_Id", newName: "eventId");
            DropPrimaryKey("dbo.Reservations");
            AlterColumn("dbo.Reservations", "eventId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Reservations", new[] { "Name", "eventId" });
            CreateIndex("dbo.Reservations", "eventId");
            AddForeignKey("dbo.Reservations", "eventId", "dbo.Events", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "eventId", "dbo.Events");
            DropIndex("dbo.Reservations", new[] { "eventId" });
            DropPrimaryKey("dbo.Reservations");
            AlterColumn("dbo.Reservations", "eventId", c => c.Int());
            AddPrimaryKey("dbo.Reservations", "Name");
            RenameColumn(table: "dbo.Reservations", name: "eventId", newName: "eventId_Id");
            CreateIndex("dbo.Reservations", "eventId_Id");
            AddForeignKey("dbo.Reservations", "eventId_Id", "dbo.Events", "Id");
        }
    }
}
