namespace EventReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reviewmodelupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "User", c => c.String());
            DropColumn("dbo.Reviews", "Comment");
            DropColumn("dbo.Reviews", "NoClicks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "NoClicks", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "Comment", c => c.String());
            DropColumn("dbo.Reviews", "User");
        }
    }
}
