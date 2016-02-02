using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebsite.Models;
using MyWebsite.Models.Enitites;
using MyWebsite.Models.Repos;
using MyWebsite.Services;
using Newtonsoft.Json;

namespace MyWebsite
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.

			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				// For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets();

				// This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
				builder.AddApplicationInsightsSettings(developerMode: true);
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddApplicationInsightsTelemetry(Configuration);

			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]))
				.AddDbContext<AbsDbContext>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddMvc();
			services.AddCaching();
			services.AddSession(options => { 
				options.IdleTimeout = TimeSpan.FromMinutes(30); 
				options.CookieName = ".MyApplication";
			});

			services.AddInstance<IConfiguration>(Configuration);

			// Add application services.
			services.AddTransient<IEmailSender, DefaultEmailSender>();
			services.AddTransient<ISmsSender, AuthMessageSender>();

			services.AddTransient<DbContext, AbsDbContext>();
            services.AddTransient<AbsDbContext, AbsDbContext>();

            services.AddTransient<IAbsRepo<Tag>, AbsRepo<Tag>>();
			services.AddTransient<IAbsRepo<Project>, AbsRepo<Project>>();
			services.AddTransient<IAbsRepo<ProjectTag>, AbsRepo<ProjectTag>>();
			services.AddTransient<IProjectsRepo, ProjectsRepo>();
			services.AddTransient<ICoursesRepo, CoursesRepo>();
			services.AddTransient<IAbsRepo<Feedback>, AbsRepo<Feedback>>();
			services.AddTransient<IAbsRepo<Contact>, AbsRepo<Contact>>();
			services.AddTransient<IAbsRepo<Leaderboard>, AbsRepo<Leaderboard>>();
			services.AddTransient<IAbsRepo<Gamestat>, AbsRepo<Gamestat>>();
			services.AddTransient<IAbsRepo<NickNameId>, AbsRepo<NickNameId>>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				Formatting = Newtonsoft.Json.Formatting.Indented,
				ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			};

			app.UseApplicationInsightsRequestTelemetry();

			app.UseBrowserLink();
			app.UseDeveloperExceptionPage();
			app.UseDatabaseErrorPage();
/*
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");

				// For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
				try
				{
					using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
						.CreateScope())
					{
						serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
							 .Database.Migrate();
					}
				}
				catch { }
			}
*/
			app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseStaticFiles();

			app.UseIdentity();
			
			app.UseSession();

			// To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

			//app.UseInMemorySession(configure: s => s.IdleTimeout = TimeSpan.FromMinutes(30));

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
