﻿using System.Globalization;
using System.Web;
using Microsoft.AspNetCore.Localization;
using SalesWebMVC.Controllers;

namespace SalesWebMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found."), opt => opt.MigrationsAssembly("SalesWebMVC")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add data service for seeding
            builder.Services.AddScoped<SeedingService>();

            // Add seller service to the scope
            builder.Services.AddScoped<SellerService>();

            // Add department service to the scope
            builder.Services.AddScoped<DepartmentService>();

            // Add sales record service to the scope
            builder.Services.AddScoped<SalesRecordService>();

            // Add GitHub profile service to the scope
            builder.Services.AddScoped<GitHubService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Applying default localization and formatting (en-US)
            var enUS = new CultureInfo("en-US");
            app.UseRequestLocalization(
                new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(enUS),
                    SupportedCultures = [enUS],
                    SupportedUICultures = [enUS]

                }
            );

            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var seedingService = services.GetRequiredService<SeedingService>();
                seedingService.Seed();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "custom",
                pattern: "About/{action}/{id?}",
                defaults: new
                {
                    controller = "GitHub",
                    action = "Index",
                });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
