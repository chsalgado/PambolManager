namespace PambolManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsScoreSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Scores", "MatchId", "dbo.Matches");
            DropIndex("dbo.Scores", new[] { "MatchId" });
            AddColumn("dbo.Matches", "HomeGoals", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "AwayGoals", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "IsScoreSet", c => c.Boolean(nullable: false));
            DropTable("dbo.Scores");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeScore = c.Int(nullable: false),
                        AwayScore = c.Int(nullable: false),
                        MatchId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Matches", "IsScoreSet");
            DropColumn("dbo.Matches", "AwayGoals");
            DropColumn("dbo.Matches", "HomeGoals");
            CreateIndex("dbo.Scores", "MatchId");
            AddForeignKey("dbo.Scores", "MatchId", "dbo.Matches", "Id", cascadeDelete: true);
        }
    }
}
