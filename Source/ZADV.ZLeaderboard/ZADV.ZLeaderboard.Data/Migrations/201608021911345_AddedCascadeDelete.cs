namespace ZADV.ZLeaderboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Voter", "Participant_Id", "dbo.Participant");
            DropIndex("dbo.Voter", new[] { "Participant_Id" });
            AlterColumn("dbo.Voter", "Participant_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Voter", "Participant_Id");
            AddForeignKey("dbo.Voter", "Participant_Id", "dbo.Participant", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voter", "Participant_Id", "dbo.Participant");
            DropIndex("dbo.Voter", new[] { "Participant_Id" });
            AlterColumn("dbo.Voter", "Participant_Id", c => c.Int());
            CreateIndex("dbo.Voter", "Participant_Id");
            AddForeignKey("dbo.Voter", "Participant_Id", "dbo.Participant", "Id");
        }
    }
}
