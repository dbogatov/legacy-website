using Microsoft.Data.Entity;
using System;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.Configuration;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models {
	public class ProjectsDbContext : DbContext {
		private readonly IConfiguration _configuration;

		public ProjectsDbContext(IConfiguration configuration) {
			_configuration = configuration;
		}

		public DbSet<Tags> Tags { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) {
			options.UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"]);
		}

	}
}
