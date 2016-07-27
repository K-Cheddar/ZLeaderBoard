namespace ZADV.ZLeaderboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventDesNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Event", "Description", c => c.String(nullable: false));
        }
    }
}
