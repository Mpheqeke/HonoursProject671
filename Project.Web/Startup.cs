using EntityConfigurationBase;
using Project.Core.Interfaces;
using Project.Core.Services;
using Project.Core.Utilities;
using Project.Infrastructure.Database;
using Project.Infrastructure.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Caching.Memory;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Project.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ProjectContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddCors(options =>
			{
				options.AddPolicy(MyAllowSpecificOrigins,
					builder =>
					{
						builder
							.AllowAnyOrigin()
							.AllowCredentials()
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.Configure<FormOptions>(options =>
			{
				options.MemoryBufferThreshold = Int32.MaxValue;
				//-----------------------------
				options.ValueLengthLimit = int.MaxValue;
				options.MultipartBodyLengthLimit = int.MaxValue;
				//-----------------------------
			});


			services.AddScoped<IProjectContext, ProjectContext>();
			services.AddScoped<IProjectUnitOfWork, ProjectUnitOfWork>();
			services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddScoped<IUserService, UserService>();
			services.AddScoped<ICompanyService, CompanyService>();
			//services.AddScoped<IAuthInfo, AuthInfo>();
			//Firebase
			//services.AddScoped<IUserAuthService, FireBaseAuth>();
			//services.AddScoped<IFireBaseHelper, FirebaseHelper>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables()
				.Build();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseCors(MyAllowSpecificOrigins);

			app.UseHttpsRedirection();
			app.UseMvc();
		}


	}
	
}
