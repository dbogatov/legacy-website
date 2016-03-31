using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MyWebsite.Models;

namespace MyWebsite.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20160331134540_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("Relational:DefaultSchema", "dbogatov");

            modelBuilder.Entity("MyWebsite.Models.Enitites.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("Email");

                    b.Property<string>("Hash");

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.Property<DateTime>("RegTime");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Course", b =>
                {
                    b.Property<string>("Title");

                    b.Property<string>("CourseId");

                    b.Property<string>("GradeLetter");

                    b.Property<double>("GradePercent");

                    b.Property<int>("ReqId");

                    b.Property<string>("Status");

                    b.Property<string>("Term");

                    b.Property<int>("Year");

                    b.HasKey("Title");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<string>("Email");

                    b.Property<string>("Subject");

                    b.Property<string>("Url");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Gamestat", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GamesPlayed");

                    b.Property<int>("GamesWon");

                    b.Property<long>("MSStart");

                    b.HasKey("UserId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Leaderboard", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("Duration");

                    b.Property<int>("Mode");

                    b.HasKey("UserId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.NickNameId", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserNickName");

                    b.HasKey("UserId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.ParentRequirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DisplayColumn");

                    b.Property<string>("Title");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.PentagoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateEnded");

                    b.Property<DateTime?>("DateStarted");

                    b.Property<string>("GameCode");

                    b.Property<string>("GameField");

                    b.Property<bool>("HostIsCross");

                    b.Property<string>("HostKey");

                    b.Property<string>("HostName");

                    b.Property<bool?>("IsGameEnded");

                    b.Property<bool>("IsHostTurn");

                    b.Property<string>("JoinKey");

                    b.Property<string>("JoinName");

                    b.Property<string>("LastTurn");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCompleted");

                    b.Property<string>("DescriptionText");

                    b.Property<string>("ImgeFilePath");

                    b.Property<string>("SourceLink");

                    b.Property<string>("Title");

                    b.Property<string>("Weblink");

                    b.HasKey("ProjectId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.ProjectTag", b =>
                {
                    b.Property<int>("RelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProjectId");

                    b.Property<int>("TagId");

                    b.HasKey("RelId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Requirement", b =>
                {
                    b.Property<int>("ReqId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ParentReqId");

                    b.Property<string>("ReqName");

                    b.HasKey("ReqId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName");

                    b.HasKey("TagId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Course", b =>
                {
                    b.HasOne("MyWebsite.Models.Enitites.Requirement")
                        .WithMany()
                        .HasForeignKey("ReqId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Leaderboard", b =>
                {
                    b.HasOne("MyWebsite.Models.Enitites.NickNameId")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyWebsite.Models.Enitites.Requirement", b =>
                {
                    b.HasOne("MyWebsite.Models.Enitites.ParentRequirement")
                        .WithMany()
                        .HasForeignKey("ParentReqId");
                });
        }
    }
}
