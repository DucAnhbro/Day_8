using Microsoft.EntityFrameworkCore;
using Ntq.Training.App.Filters;
using Day_8.Middleware;
using Day_8.Dto;
using Day_8.Models;
using System.Net;

namespace Day_8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //string conn = builder.Configuration.GetConnectionString("Conn");
            // Add services to the container.
            builder.Services.AddSession();

            builder.Services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<Authorization>();
                //options.Filters.Add<LoggingFilter>();
            });
            //builder.Services.AddDbContext<RoleUserAppContext>(options =>
            //{
            //    options.UseSqlServer(conn);
            //});
            builder.Services.AddSession();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin",
                     policy => policy.RequireRole("Create user"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            //app.UseMiddleware<AuthenMiddleware>();
            //app.UseMiddleware<RequestMiddleware>();
            //app.UseMiddleware<ReponseMiddleware>();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}