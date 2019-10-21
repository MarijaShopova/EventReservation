namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracija2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "NoClicks", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "NoClicks");
        }
    }
}
