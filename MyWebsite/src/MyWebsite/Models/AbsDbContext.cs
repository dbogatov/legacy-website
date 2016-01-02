using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using MyWebsite.Models.Enitites;

namespace MyWebsite.Models {
	public class AbsDbContext : DbContext {
		private readonly IConfiguration _configuration;

		public AbsDbContext(IConfiguration configuration) {
			_configuration = configuration;
		}

		public DbSet<Tag> Tags { get; set; }
		public DbSet<Project> Projects { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) {
			options.UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"]);
		}

	}
}
