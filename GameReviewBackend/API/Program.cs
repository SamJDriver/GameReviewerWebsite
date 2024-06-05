using BusinessLogic.Abstractions;
using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Microsoft.EntityFrameworkCore;
using Repositories;
using API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;

namespace GameReview
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<Program>()
                .Build();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSwaggerGen( c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DGC Game Review API",
                    Version = "v1",
                    Description = "An API created by Team Dominion Gaming Company to Manage Games and Reviews",
                    // TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "DGC",
                    }
                });

                var scopes = new Dictionary<string, string>();
                scopes.Add("https://graph.microsoft.com/.default", "Access application on user behalf");

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                          },
                          Scheme = "oauth2",
                          Name = "oauth2",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("https://login.microsoftonline.com/common/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri("https://login.microsoftonline.com/common/oauth2/v2.0/token"),
                            Scopes = scopes,
                        }
                    },
                    In = ParameterLocation.Header,
                    Name = "Authorization"

 
                    // Type = SecuritySchemeType.ApiKey
                });
                // c.OperationFilter<SecurityRequirementsOperationFilter>();

                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);
            });

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

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
              options.CheckConsentNeeded = context => false;
              options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.Audience = config["ClientSecrets:SwaggerClient:ResourceId"]; //Receiving token from Azure Entra
                    options.Authority = config["ClientSecrets:SwaggerClient:InstanceId"] + config["ClientSecrets:SwaggerClient:TenantId"]; //Sends out tokens
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options => {
                    options.SignInScheme = "Cookies";
                    options.Authority = config["ClientSecrets:SwaggerClient:InstanceId"] + config["ClientSecrets:SwaggerClient:TenantId"];
                    options.ClientId = config["ClientSecrets:SwaggerClient:ClientId"];
                    options.ResponseType = "code";
                    options.Prompt = "login";
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                });

            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme);
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IIgdbApiService, IgdbApiService>();
            builder.Services.AddScoped<IPlayRecordService, PlayRecordService>();
            builder.Services.AddScoped<IPlayRecordCommentService, PlayRecordCommentService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped(typeof(GenericRepository<>));
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
            
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

                options.OAuthAppName("Swagger Client");
                options.OAuthClientId($"{config["ClientSecrets:SwaggerClient:ClientId"]}");
                options.OAuthClientSecret($"{config["ClientSecrets:SwaggerClient:ClientSecret"]}");
                options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                options.OAuth2RedirectUrl("https://jwt.ms");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.UseMiddleware<SwaggerOAuthMiddleware>();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}