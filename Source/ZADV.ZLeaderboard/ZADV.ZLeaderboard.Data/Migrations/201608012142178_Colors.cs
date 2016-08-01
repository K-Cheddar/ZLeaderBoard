namespace ZADV.ZLeaderboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Colors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participant", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participant", "Color");
        }
    }
}
