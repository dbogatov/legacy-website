using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebsite.Models;
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
				.AddDbContext<DataContext>();

			DataContext.connectionString = Configuration["DataAccessPostgreSqlProvider:AnothCS"]; 
			//Configuration["Data:DefaultConnection:ConnectionString"];

			services.AddMvc();
			services.AddCaching();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.CookieName = ".MyApplication";
			});

			services.AddInstance<IConfiguration>(Configuration);

			// Add application services.
			services.AddTransient<IEmailSender, DefaultEmailSender>();
			services.AddTransient<DataContext, DataContext>();
			services.AddTransient<ICryptoService, CryptoService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
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

			app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseStaticFiles();

			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
			
			using(var context = serviceProvider.GetService<DataContext>())
			{
				context.Database.EnsureCreated();
				context.EnsureSeedData();
			}
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
