using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SKIPQzAPI.DataAccess;
using AutoMapper;
using SKIPQzAPI.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace SKIPQzAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ServiceProviderService>();
            services.AddScoped<ServiceService>();
            services.AddScoped<BookingService>();
            services.AddCors(options =>
            {
                options.AddPolicy("CrossOriginAccess", config =>
                {
                    config.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // For the wwwroot folder.

            // using Microsoft.Extensions.FileProviders;
            // using System.IO;
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "wwwroot\\Images")),
                RequestPath = "/Images",
                EnableDirectoryBrowsing = true
            });
            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("CrossOriginAccess");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
