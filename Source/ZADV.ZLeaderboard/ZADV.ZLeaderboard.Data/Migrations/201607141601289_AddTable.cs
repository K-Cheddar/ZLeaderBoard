namespace ZADV.ZLeaderboard.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Voter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 500),
                        Participant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participant", t => t.Participant_Id)
                .Index(t => t.Participant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voter", "Participant_Id", "dbo.Participant");
            DropIndex("dbo.Voter", new[] { "Participant_Id" });
            DropTable("dbo.Voter");
        }
    }
}
