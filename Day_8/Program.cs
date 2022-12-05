using Microsoft.EntityFrameworkCore;
using Day_8.Filters;
using Day_8.Middleware;
using Day_8.Dto;
using Day_8.Models;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Day_8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string conn = builder.Configuration.GetConnectionString("Conn");
            //add services to the container.
            builder.Services.AddSession();
            builder.Services.AddControllersWithViews(options =>
            { 
            });
            builder.Services.AddDbContext<EmployessContext>(options =>
            {
                options.UseSqlServer(conn);
            });
            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie("DemoSecurityScheme", options =>
   {
       options.AccessDeniedPath = new PathString("/Account/Access");
       options.Cookie = new CookieBuilder
       {
           //Domain = "",
           HttpOnly = true,
           Name = ".aspNetCoreDemo.Security.Cookie",
           Path = "/",
           SameSite = SameSiteMode.Lax,
           SecurePolicy = CookieSecurePolicy.SameAsRequest
       };
       options.Events = new CookieAuthenticationEvents
       {
           OnSignedIn = context =>
           {
               Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                   "OnSignedIn", context.Principal.Identity.Name);
               return Task.CompletedTask;
           },
           OnSigningOut = context =>
           {
               Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                   "OnSigningOut", context.HttpContext.User.Identity.Name);
               return Task.CompletedTask;
           },
           OnValidatePrincipal = context =>
           {
               Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
                   "OnValidatePrincipal", context.Principal.Identity.Name);
               return Task.CompletedTask;
           }
       };
       //options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
       options.LoginPath = new PathString("/Account/Login");
       options.ReturnUrlParameter = "RequestPath";
       options.SlidingExpiration = true;
   });
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<LoggingFilter>();
            });
            builder.Services.AddDbContext<EmployessContext>(options =>
            {
                options.UseSqlServer(conn);
            });
            builder.Services.AddSession();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin",
                     policy => policy.RequireRole("Create Employe"));
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