using System.Runtime.InteropServices;
using BusinessLogic.Abstractions;
using BusinessLogic.Infrastructure;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace GameReview
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSwaggerGen();

            //If working locally, default environment variables to localhost values
            var serverName = Environment.GetEnvironmentVariable("MYSQL_SERVICE_NAME") ?? "127.0.0.1";
            var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
            var databaseName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "mydatabase";
            var username = Environment.GetEnvironmentVariable("MYSQL_USER") ?? "user";
            var password_file_path = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            var password = "password";
            if (password_file_path != null) 
            {
                password = File.ReadAllText(@$"{password_file_path}");
            }
            
            
            

            


            var connectionString = $"Server={serverName}; Port={port}; Database={databaseName}; Uid={username}; Pwd={password}";
            builder.Services.AddDbContext<DockerDbContext>(
                options => options
                .UseLazyLoadingProxies()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
           ).EnableDetailedErrors());

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IIgdbApiService, IgdbApiService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped(typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/error-development");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}