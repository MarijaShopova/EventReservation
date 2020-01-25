namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bla : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Genre", c => c.String(nullable: false));

        }
        
        public override void Down()
        {
            
            AlterColumn("dbo.Events", "Genre", c => c.String());
        }
    }
}
