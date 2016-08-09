namespace ZADV.ZLeaderboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotingScheme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "MultipleVotes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "MultipleVotes");
        }
    }
}
