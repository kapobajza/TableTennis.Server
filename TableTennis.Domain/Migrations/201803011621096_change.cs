namespace TableTennis.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        ExceptionLogId = c.Int(nullable: false, identity: true),
                        ExceptionName = c.String(),
                        RequestMethod = c.String(),
                        RequestUri = c.String(),
                        RequestContent = c.String(),
                        ExceptionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExceptionLogId);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        TeamOneId = c.Int(nullable: false),
                        TeamTwoId = c.Int(nullable: false),
                        DateStarted = c.DateTime(nullable: false),
                        DateEnded = c.DateTime(nullable: false),
                        Duration = c.String(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Teams", t => t.TeamOneId)
                .ForeignKey("dbo.Teams", t => t.TeamTwoId)
                .Index(t => t.TeamOneId)
                .Index(t => t.TeamTwoId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserTeams",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        DateJoined = c.DateTime(nullable: false),
                        IsLeader = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TeamId })
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTeams", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "TeamTwoId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "TeamOneId", "dbo.Teams");
            DropIndex("dbo.UserTeams", new[] { "TeamId" });
            DropIndex("dbo.UserTeams", new[] { "UserId" });
            DropIndex("dbo.Matches", new[] { "TeamTwoId" });
            DropIndex("dbo.Matches", new[] { "TeamOneId" });
            DropTable("dbo.UserTeams");
            DropTable("dbo.Users");
            DropTable("dbo.Teams");
            DropTable("dbo.Matches");
            DropTable("dbo.ExceptionLogs");
        }
    }
}
