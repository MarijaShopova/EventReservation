namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class column_rename : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LocalImages", "LocalId", "dbo.Locals");
            DropIndex("dbo.LocalImages", new[] { "LocalId" });
            RenameColumn(table: "dbo.LocalImages", name: "LocalId", newName: "LocalId");
            AlterColumn("dbo.LocalImages", "LocalId", c => c.Int(nullable: false));
            CreateIndex("dbo.LocalImages", "LocalId");
            AddForeignKey("dbo.LocalImages", "LocalId", "dbo.Locals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalImages", "LocalId", "dbo.Locals");
            DropIndex("dbo.LocalImages", new[] { "LocalId" });
            AlterColumn("dbo.LocalImages", "LocalId", c => c.Int());
            RenameColumn(table: "dbo.LocalImages", name: "LocalId", newName: "LocalId");
            CreateIndex("dbo.LocalImages", "LocalId");
            AddForeignKey("dbo.LocalImages", "LocalId", "dbo.Locals", "Id");
        }
    }
}
