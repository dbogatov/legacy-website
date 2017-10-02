using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWebsite.Models;
using MyWebsite.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MyWebsite
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.

			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				// For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets();
			}

			//builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.

			// services
			// 	.AddEntityFrameworkNpgsql()
			// 	.AddDbContext<DataContext>();

			services
					.AddEntityFrameworkInMemoryDatabase()
					.AddDbContext<DataContext>(
						b => b
							.UseInMemoryDatabase()
							.UseInternalServiceProvider(
								new ServiceCollection()
									.AddEntityFrameworkInMemoryDatabase()
									.BuildServiceProvider()
							)
					);


			// DataContext.connectionString = Configuration["DataAccessPostgreSqlProvider:ConnectionString"];

			services.AddMvc().AddJsonOptions(opt =>
			{
				var resolver = opt.SerializerSettings.ContractResolver;
				if (resolver != null)
				{
					var res = resolver as DefaultContractResolver;
					res.NamingStrategy = null;  // <<!-- this removes the camelcasing
				}
			});

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.CookieName = ".MyApplication";
			});

			services.AddCors(options =>
			{
				options.AddPolicy(
					"AllowSpecificOrigin",
					builder => builder.WithOrigins(
						"http://visasupport.kiev.ua",
						"http://lp.visasupport.kiev.ua",
						"http://eu.visasupport.kiev.ua",
						"http://darinagulley.com"
					));
			});

			// Add application services.
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddTransient<IEmailSender, DefaultEmailSender>();
			services.AddTransient<DataContext, DataContext>();
			services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IPushService, PushService>();
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

			app.UseDeveloperExceptionPage();
			app.UseDatabaseErrorPage();

			app.UseStaticFiles();

			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			using (var context = serviceProvider.GetService<DataContext>())
			{
				context.Database.EnsureCreated();
				context.EnsureSeedData();
			}
		}

		// Entry point for the application.
		public static void Main(string[] args)
		{
			var host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.UseUrls("http://*:5001")
				.Build();

			host.Run();
		}
	}
}
