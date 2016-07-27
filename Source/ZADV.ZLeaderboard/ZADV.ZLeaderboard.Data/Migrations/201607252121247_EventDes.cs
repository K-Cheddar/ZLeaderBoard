namespace ZADV.ZLeaderboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventDes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "Description");
        }
    }
}
