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
		public DbSet<Requirement> Requirement { get; set; }

		public DbSet<Feedback> Feedbacks { get; set; }

		public DbSet<Contact> Contacts { get; set; }

		public DbSet<Leaderboard> Leaderboards { get; set; }
		public DbSet<Gamestat> Gamestats { get; set; }
		public DbSet<NickNameId> NickNameIds { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(DataContext.connectionString);
			//options.UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"]);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Course>().HasOne(c => c.Requirement).WithMany().HasForeignKey(c => c.ReqId);
			modelBuilder.Entity<Requirement>().HasOne(r => r.ParentRequirement).WithMany().HasForeignKey(c => c.parentReqId);
			modelBuilder.Entity<Leaderboard>().HasOne(l => l.NickNameId).WithMany().HasForeignKey(l => l.UserId);
		}

	}
}
