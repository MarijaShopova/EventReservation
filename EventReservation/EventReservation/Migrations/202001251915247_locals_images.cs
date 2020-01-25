namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class locals_images : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.LocalImages", c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LocalId = c.Int(nullable: false),
                    Image = c.Binary(nullable: false, storeType: "image")
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locals", t => t.LocalId, cascadeDelete: true)
                .Index(t => t.LocalId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.LocalImages", "LocalId", "dbo.Locals");
            DropIndex("dbo.LocalImages", new[] { "LocalId" });
            DropTable("dbo.LocalImages");
        }
    }
}
