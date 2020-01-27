namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class localidtoreview : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Local_Id", "dbo.Locals");
            DropIndex("dbo.Reviews", new[] { "Local_Id" });
            RenameColumn(table: "dbo.Reviews", name: "Local_Id", newName: "localId");
            AlterColumn("dbo.Reviews", "localId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "localId");
            AddForeignKey("dbo.Reviews", "localId", "dbo.Locals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "localId", "dbo.Locals");
            DropIndex("dbo.Reviews", new[] { "localId" });
            AlterColumn("dbo.Reviews", "localId", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "localId", newName: "Local_Id");
            CreateIndex("dbo.Reviews", "Local_Id");
            AddForeignKey("dbo.Reviews", "Local_Id", "dbo.Locals", "Id");
        }
    }
}
