using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public static string connectionString;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataContext() { }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }


        public DbSet<Course> Courses { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<ParentRequirement> ParentRequirements { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Gamestat> Gamestats { get; set; }
        public DbSet<NickNameId> NickNameIds { get; set; }

        public DbSet<PentagoGame> PentagoGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseInMemoryDatabase();
            options.UseNpgsql(DataContext.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasOne(c => c.Requirement).WithMany().HasForeignKey(c => c.ReqId);
            modelBuilder.Entity<Requirement>().HasOne(r => r.ParentRequirement).WithMany().HasForeignKey(c => c.ParentReqId);
            modelBuilder.Entity<Leaderboard>().HasOne(l => l.NickNameId).WithMany().HasForeignKey(l => l.UserId);

            modelBuilder.HasDefaultSchema("dbogatov");
            base.OnModelCreating(modelBuilder);
        }

        public void EnsureSeedData()
        {
            var courses = new List<Course> {
                new Course
                {
                    Title = "3D Modeling I",
                    Term = "C",
                    Year = 2016,
                    CourseId = "AR2102",
                    GradePercent = 89.5,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 5
                },
                new Course
                {
                    Title = "Algorithms",
                    Term = "D",
                    Year = 2015,
                    CourseId = "CS2223",
                    GradePercent = 97,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Applied Statistics I",
                    Term = "A",
                    Year = 2014,
                    CourseId = "MA2611",
                    GradePercent = 95.72,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 2
                },
                new Course
                {
                    Title = "Calculus II",
                    Term = "A",
                    Year = 2013,
                    CourseId = "MA1022",
                    GradePercent = 97.4,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 2
                },
                new Course
                {
                    Title = "Calculus III",
                    Term = "B",
                    Year = 2013,
                    CourseId = "MA1023",
                    GradePercent = 95.7,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 2
                },
                new Course
                {
                    Title = "Calculus IV",
                    Term = "C",
                    Year = 2014,
                    CourseId = "MA1024",
                    GradePercent = 96.02,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 2
                },
                new Course
                {
                    Title = "Data Mining and Knowledge Discovery in Databases",
                    Term = "B",
                    Year = 2015,
                    CourseId = "CS4445",
                    GradePercent = 98.6,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Database Systems I",
                    Term = "C",
                    Year = 2015,
                    CourseId = "CS3431",
                    GradePercent = 91.7,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Digital Image",
                    Term = "B",
                    Year = 2014,
                    CourseId = "AR1101",
                    GradePercent = 97.65,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 5
                },
                new Course
                {
                    Title = "Discrete Mathematics",
                    Term = "A",
                    Year = 2014,
                    CourseId = "CS2202",
                    GradePercent = 93.5,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Distributed Computing Systems",
                    Term = "C",
                    Year = 2016,
                    CourseId = "CS4513",
                    GradePercent = 93,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Elements of Writing",
                    Term = "D",
                    Year = 2015,
                    CourseId = "WR1010",
                    GradePercent = 93,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 4
                },
                new Course
                {
                    Title = "Essentials of Art",
                    Term = "C",
                    Year = 2014,
                    CourseId = "AR1100",
                    GradePercent = 96.7,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 5
                },
                new Course
                {
                    Title = "Intermediate Microeconomics",
                    Term = "D",
                    Year = 2016,
                    CourseId = "ECON2110",
                    GradePercent = 90,
                    GradeLetter = "I",
                    Status = "Registered",
                    ReqId = 10
                },
                new Course
                {
                    Title = "Introduction to Electrical and Computer Engineering",
                    Term = "A",
                    Year = 2016,
                    CourseId = "ECE2010",
                    GradePercent = 90,
                    GradeLetter = "I",
                    Status = "Registered",
                    ReqId = 3
                },
                new Course
                {
                    Title = "Introduction to History of Technology",
                    Term = "A",
                    Year = 2015,
                    CourseId = "HI1332",
                    GradePercent = 93.4,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 4
                },
                new Course
                {
                    Title = "Introduction to Object Oriented Programming",
                    Term = "B",
                    Year = 2013,
                    CourseId = "CS2102",
                    GradePercent = 91.4,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Introduction to Program Design",
                    Term = "A",
                    Year = 2013,
                    CourseId = "CS1101",
                    GradePercent = 97.6,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "IQP Term B",
                    Term = "B",
                    Year = 2014,
                    CourseId = "HH1423",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 9
                },
                new Course
                {
                    Title = "IQP Term C",
                    Term = "C",
                    Year = 2015,
                    CourseId = "HH1423",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 9
                },
                new Course
                {
                    Title = "IQP Term D",
                    Term = "D",
                    Year = 2015,
                    CourseId = "HH1423",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 9
                },
                new Course
                {
                    Title = "ISP Trading Systems, Investment & Risk Management",
                    Term = "A",
                    Year = 2014,
                    CourseId = "HH2000",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 7
                },
                new Course
                {
                    Title = "Macroeconomics",
                    Term = "D",
                    Year = 2014,
                    CourseId = "ECON1120",
                    GradePercent = 93.33,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 7
                },
                new Course
                {
                    Title = "Microeconomics",
                    Term = "B",
                    Year = 2015,
                    CourseId = "ECON1110",
                    GradePercent = 99.79,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 10
                },
                new Course
                {
                    Title = "Molecularity",
                    Term = "C",
                    Year = 2015,
                    CourseId = "CH1010",
                    GradePercent = 93.05,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 3
                },
                new Course
                {
                    Title = "MQP Term A",
                    Term = "A",
                    Year = 2015,
                    CourseId = "EAR1501",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "MQP Term B",
                    Term = "B",
                    Year = 2015,
                    CourseId = "EAR1501",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "MQP Term C",
                    Term = "C",
                    Year = 2016,
                    CourseId = "EAR1501",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Operating Systems",
                    Term = "A",
                    Year = 2015,
                    CourseId = "CS3013",
                    GradePercent = 90.4,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Ordinary Differential Equations",
                    Term = "D",
                    Year = 2014,
                    CourseId = "MA2051",
                    GradePercent = 98.5,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 2
                },
                new Course
                {
                    Title = "Physics: General Electricity",
                    Term = "B",
                    Year = 2013,
                    CourseId = "PH1120",
                    GradePercent = 96,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 3
                },
                new Course
                {
                    Title = "Physics: General Mechanics",
                    Term = "A",
                    Year = 2013,
                    CourseId = "PH1110",
                    GradePercent = 95.3,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 3
                },
                new Course
                {
                    Title = "Physics: Intermediate Mechanics I",
                    Term = "A",
                    Year = 2014,
                    CourseId = "PH2201",
                    GradePercent = 90.55,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 3
                },
                new Course
                {
                    Title = "Practicum in Humanities: Visual Persuasion",
                    Term = "D",
                    Year = 2016,
                    CourseId = "HU3910",
                    GradePercent = 90,
                    GradeLetter = "I",
                    Status = "Registered",
                    ReqId = 6
                },
                new Course
                {
                    Title = "Probability for Applications",
                    Term = "C",
                    Year = 2015,
                    CourseId = "MA2621",
                    GradePercent = 88.6,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 2
                },
                new Course
                {
                    Title = "Programming Languages",
                    Term = "D",
                    Year = 2016,
                    CourseId = "CS4536",
                    GradePercent = 90,
                    GradeLetter = "I",
                    Status = "Registered",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Soccer Term B",
                    Term = "B",
                    Year = 2015,
                    CourseId = "PE1019",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 8
                },
                new Course
                {
                    Title = "Soccer Term D",
                    Term = "D",
                    Year = 2015,
                    CourseId = "PE1019",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 8
                },
                new Course
                {
                    Title = "Software Engineering",
                    Term = "D",
                    Year = 2014,
                    CourseId = "CS3733",
                    GradePercent = 89.95,
                    GradeLetter = "B",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Systems Programming",
                    Term = "C",
                    Year = 2014,
                    CourseId = "CS2303",
                    GradePercent = 94.4,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                },
                new Course
                {
                    Title = "Volleyball",
                    Term = "A",
                    Year = 2016,
                    CourseId = "PE1002",
                    GradePercent = 90,
                    GradeLetter = "I",
                    Status = "Registered",
                    ReqId = 8
                },
                new Course
                {
                    Title = "Volleyball & Squash",
                    Term = "B",
                    Year = 2014,
                    CourseId = "PE1002",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 8
                },
                new Course
                {
                    Title = "Webware: Computational Technology for Network Inforamtion Systems",
                    Term = "B",
                    Year = 2015,
                    CourseId = "CS4241",
                    GradePercent = 100,
                    GradeLetter = "A",
                    Status = "Completed",
                    ReqId = 1
                }
            };

            var requirements = new List<Requirement> {
                new Requirement { ReqId = 1, ReqName = "CS Classes + MQP", ParentReqId = 1 },
                new Requirement { ReqId = 2, ReqName = "Math", ParentReqId = 1 },
                new Requirement { ReqId = 3, ReqName = "Science", ParentReqId = 1 },
                new Requirement { ReqId = 4, ReqName = "Breadth", ParentReqId = 3 },
                new Requirement { ReqId = 5, ReqName = "Depth", ParentReqId = 3 },
                new Requirement { ReqId = 6, ReqName = "Seminar", ParentReqId = 3 },
                new Requirement { ReqId = 7, ReqName = "Social Sciences", ParentReqId = 2 },
                new Requirement { ReqId = 8, ReqName = "Physical Education", ParentReqId = 4 },
                new Requirement { ReqId = 9, ReqName = "IQP", ParentReqId = 5 },
                new Requirement { ReqId = 10, ReqName = "Economics Minor", ParentReqId = 6 }
            };

            var parentRequirements = new List<ParentRequirement> {
                new ParentRequirement { Id = 1, Title = "Major Requirements", DisplayColumn = 1 },
                new ParentRequirement { Id = 2, Title = "Social Requirements", DisplayColumn = 2 },
                new ParentRequirement { Id = 3, Title = "Humanities Requirements", DisplayColumn = 2 },
                new ParentRequirement { Id = 4, Title = "PE Requirements", DisplayColumn = 2 },
                new ParentRequirement { Id = 5, Title = "IQP Requirements", DisplayColumn = 2 },
                new ParentRequirement { Id = 6, Title = "Minor Requirements", DisplayColumn = 2 }
            };

            var projects = new List<Project> {
                new Project
                {
                    ProjectId = 1,
                    Title = "Minesweeper",
                    DescriptionText = "The Minsweeper is a Web Application imitating a well-known Microsoft Minesweeper.",
                    DateCompleted = new DateTime(2014, 9, 30),
                    Weblink = "/Project/Minesweeper",
                    SourceLink = "https://bitbucket.org/Dima4ka/personal-website",
                    ImgeFilePath = "../images/Minesweeper.png"
                },
                new Project
                {
                    ProjectId = 2,
                    Title = "Personal Website",
                    DescriptionText = "Personal Website is a private project containing the resume, projects and blog of its author.",
                    DateCompleted = new DateTime(2015, 3, 27),
                    Weblink = "/",
                    SourceLink = "https://bitbucket.org/Dima4ka/personal-website",
                    ImgeFilePath = "../images/PersonalWebsite.png"
                },
                new Project
                {
                    ProjectId = 3,
                    Title = "Banker Game Assistant",
                    DescriptionText = "This is an iOS application serving as a banker for board games. It keeps track of all your money accounts.",
                    DateCompleted = new DateTime(2015, 2, 15),
                    Weblink = "/Project/Banker",
                    SourceLink = "https://bitbucket.org/Dima4ka/monopoly-banker/",
                    ImgeFilePath = "../images/Banker.png"
                },
                new Project
                {
                    ProjectId = 4,
                    Title = "WPI Event Creator",
                    DescriptionText = "This is an iOS application allowing WPI students to create appointment events with faculty easily and quickly.",
                    DateCompleted = new DateTime(2014, 4, 10),
                    Weblink = "/Project/WPICalendar",
                    SourceLink = "https://bitbucket.org/Dima4ka/wpi-calendar-event-creator/",
                    ImgeFilePath = "../images/WPICalendarEventCreator.png"
                },
                new Project
                {
                    ProjectId = 5,
                    Title = "Finance Parser",
                    DescriptionText = "Simple parser uses Google API to display current bid and ask prices for selected symbols and strikes.",
                    DateCompleted = new DateTime(2015, 4, 5),
                    Weblink = "/Project/GoogleFinanceParser",
                    SourceLink = "https://bitbucket.org/Dima4ka/personal-website",
                    ImgeFilePath = "../images/FinanceParser.png"
                },
                new Project
                {
                    ProjectId = 6,
                    Title = "IQP - Trading & Investment",
                    DescriptionText = "Interactive Qualifying Project - developing a system of trading systems using scientific approches.",
                    DateCompleted = new DateTime(2015, 5, 6),
                    Weblink = "/Project/IQP",
                    SourceLink = "#",
                    ImgeFilePath = "../images/IQP.jpg"
                },
                new Project
                {
                    ProjectId = 7,
                    Title = "MHTC",
                    DescriptionText = "Major Qualifying Project - MATTERS",
                    DateCompleted = new DateTime(2016, 3, 20),
                    Weblink = "http://matters.mhtc.org",
                    SourceLink = "https://github.com/WPIMHTC/MHTCDashboard",
                    ImgeFilePath = "../images/MHTC.png"
                },
                new Project
                {
                    ProjectId = 8,
                    Title = "Pentago",
                    DescriptionText = "Pentago game - Webware project",
                    DateCompleted = new DateTime(2015, 12, 17),
                    Weblink = "/Project/Pentago",
                    SourceLink = "https://bitbucket.org/Dima4ka/personal-website",
                    ImgeFilePath = "../images/Pentago.png"
                },
				new Project
                {
                    ProjectId = 9,
                    Title = "Mandelbrot",
                    DescriptionText = "Mandelbrot fractal interactive generator",
                    DateCompleted = new DateTime(2016, 05, 04),
                    Weblink = "/Project/Mandelbrot",
                    SourceLink = "https://bitbucket.org/Dima4ka/personal-website",
                    ImgeFilePath = "../images/Mandelbrot.jpg"
                }
            };

            var tags = new List<Tag> {
                new Tag { TagId = 1, TagName = "Large" },
                new Tag { TagId = 2, TagName = "University" },
				new Tag { TagId = 3, TagName = "Featured" }
            };

            var projectTags = new List<ProjectTag> {
                new ProjectTag { ProjectId = 1, TagId = 1 },
				new ProjectTag { ProjectId = 1, TagId = 3 },
				new ProjectTag { ProjectId = 2, TagId = 1 },
				new ProjectTag { ProjectId = 3, TagId = 1 },
				new ProjectTag { ProjectId = 4, TagId = 1 },
				new ProjectTag { ProjectId = 5, TagId = 2 },
				new ProjectTag { ProjectId = 6, TagId = 1 },
				new ProjectTag { ProjectId = 6, TagId = 2 },
				new ProjectTag { ProjectId = 7, TagId = 1 },
				new ProjectTag { ProjectId = 7, TagId = 2 },
				new ProjectTag { ProjectId = 8, TagId = 1 },
				new ProjectTag { ProjectId = 8, TagId = 2 },
				new ProjectTag { ProjectId = 8, TagId = 3 },
				new ProjectTag { ProjectId = 9, TagId = 1 },
				new ProjectTag { ProjectId = 9, TagId = 2 },
				new ProjectTag { ProjectId = 9, TagId = 3 },
            };

            if (this.Courses.Count() < courses.Count())
            {
                this.Courses.Clear();
                this.SaveChanges();
                this.Courses.AddRange(courses);
            }

            if (this.Requirements.Count() < requirements.Count())
            {
				this.Requirements.Clear();
				this.SaveChanges();
                this.Requirements.AddRange(requirements);
            }

            if (this.ParentRequirements.Count() < parentRequirements.Count())
            {
				this.ParentRequirements.Clear();
				this.SaveChanges();
                this.ParentRequirements.AddRange(parentRequirements);
            }

            if (this.Projects.Count() < projects.Count())
            {
				this.Projects.Clear();
				this.SaveChanges();
                this.Projects.AddRange(projects);
            }

            if (this.Tags.Count() < tags.Count())
            {
				this.Tags.Clear();
				this.SaveChanges();
                this.Tags.AddRange(tags);
            }

            if (this.ProjectTags.Count() < projectTags.Count())
            {
				this.ProjectTags.Clear();
				this.SaveChanges();
                this.ProjectTags.AddRange(projectTags);
            }

            if (!this.NickNameIds.Any())
            {
                this.NickNameIds.Add(
                    new NickNameId { UserId = 1, UserNickName = "Dima4ka" }
                );
            }

            if (!this.Leaderboards.Any())
            {
                this.Leaderboards.Add(
                    new Leaderboard { UserId = 1, Mode = 1, Duration = 15651 }
                );
            }

            this.SaveChanges();

        }

    }
}
