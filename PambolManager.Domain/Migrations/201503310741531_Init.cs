namespace PambolManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeTeamId = c.Guid(nullable: false),
                        AwayTeamId = c.Guid(nullable: false),
                        ScoreId = c.Guid(nullable: false),
                        Round_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rounds", t => t.Round_Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .ForeignKey("dbo.Scores", t => t.ScoreId, cascadeDelete: true)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
                .Index(t => t.ScoreId)
                .Index(t => t.Round_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeamName = c.String(nullable: false),
                        LogoPath = c.String(),
                        TournamentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        TeamId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TournamentName = c.String(nullable: false),
                        TotalRounds = c.Int(nullable: false),
                        MaxTeams = c.Int(nullable: false),
                        BeginDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        FieldManagerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FieldManagerId)
                .Index(t => t.FieldManagerId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LastName = c.String(),
                        FieldName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoundNumber = c.Int(nullable: false),
                        TournamentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeScore = c.Int(nullable: false),
                        AwayScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Matches", "ScoreId", "dbo.Scores");
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayTeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Rounds", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Matches", "Round_Id", "dbo.Rounds");
            DropForeignKey("dbo.Tournaments", "FieldManagerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rounds", new[] { "TournamentId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tournaments", new[] { "FieldManagerId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Teams", new[] { "TournamentId" });
            DropIndex("dbo.Matches", new[] { "Round_Id" });
            DropIndex("dbo.Matches", new[] { "ScoreId" });
            DropIndex("dbo.Matches", new[] { "AwayTeamId" });
            DropIndex("dbo.Matches", new[] { "HomeTeamId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Scores");
            DropTable("dbo.Rounds");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Players");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
        }
    }
}
