using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MyWebsite.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("dbogatov");
            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "dbogatov",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Comment = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RegTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Feedback",
                schema: "dbogatov",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Body = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Gamestat",
                schema: "dbogatov",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesWon = table.Column<int>(nullable: false),
                    MSStart = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamestat", x => x.UserId);
                });
            migrationBuilder.CreateTable(
                name: "NickNameId",
                schema: "dbogatov",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    UserNickName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NickNameId", x => x.UserId);
                });
            migrationBuilder.CreateTable(
                name: "ParentRequirement",
                schema: "dbogatov",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    DisplayColumn = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentRequirement", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "PentagoGame",
                schema: "dbogatov",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    DateEnded = table.Column<DateTime>(nullable: true),
                    DateStarted = table.Column<DateTime>(nullable: true),
                    GameCode = table.Column<string>(nullable: true),
                    GameField = table.Column<string>(nullable: true),
                    HostIsCross = table.Column<bool>(nullable: false),
                    HostKey = table.Column<string>(nullable: true),
                    HostName = table.Column<string>(nullable: true),
                    IsGameEnded = table.Column<bool>(nullable: true),
                    IsHostTurn = table.Column<bool>(nullable: false),
                    JoinKey = table.Column<string>(nullable: true),
                    JoinName = table.Column<string>(nullable: true),
                    LastTurn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PentagoGame", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Project",
                schema: "dbogatov",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    DateCompleted = table.Column<DateTime>(nullable: false),
                    DescriptionText = table.Column<string>(nullable: true),
                    ImgeFilePath = table.Column<string>(nullable: true),
                    SourceLink = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Weblink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });
            migrationBuilder.CreateTable(
                name: "ProjectTag",
                schema: "dbogatov",
                columns: table => new
                {
                    RelId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    ProjectId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => x.RelId);
                });
            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "dbogatov",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });
            migrationBuilder.CreateTable(
                name: "Leaderboard",
                schema: "dbogatov",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboard", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Leaderboard_NickNameId_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbogatov",
                        principalTable: "NickNameId",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Requirement",
                schema: "dbogatov",
                columns: table => new
                {
                    ReqId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    ParentReqId = table.Column<int>(nullable: false),
                    ReqName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.ReqId);
                    table.ForeignKey(
                        name: "FK_Requirement_ParentRequirement_ParentReqId",
                        column: x => x.ParentReqId,
                        principalSchema: "dbogatov",
                        principalTable: "ParentRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Course",
                schema: "dbogatov",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    CourseId = table.Column<string>(nullable: true),
                    GradeLetter = table.Column<string>(nullable: true),
                    GradePercent = table.Column<double>(nullable: false),
                    ReqId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Term = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Title);
                    table.ForeignKey(
                        name: "FK_Course_Requirement_ReqId",
                        column: x => x.ReqId,
                        principalSchema: "dbogatov",
                        principalTable: "Requirement",
                        principalColumn: "ReqId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Contact", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Course", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Feedback", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Gamestat", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Leaderboard", schema: "dbogatov");
            migrationBuilder.DropTable(name: "PentagoGame", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Project", schema: "dbogatov");
            migrationBuilder.DropTable(name: "ProjectTag", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Tag", schema: "dbogatov");
            migrationBuilder.DropTable(name: "Requirement", schema: "dbogatov");
            migrationBuilder.DropTable(name: "NickNameId", schema: "dbogatov");
            migrationBuilder.DropTable(name: "ParentRequirement", schema: "dbogatov");
        }
    }
}
