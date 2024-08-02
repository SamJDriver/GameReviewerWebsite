using API.Middlewares;
using API.Webframework;
using BusinessLogic.Abstractions;
using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Repositories;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Graph;
using Azure.Identity;

namespace GameReview
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            bool.TryParse(Environment.GetEnvironmentVariable("RUNNING_IN_CONTAINER_FLAG"), out bool containerFlag);

            IConfiguration? config = default;

            if (containerFlag)
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile(Environment.GetEnvironmentVariable("IGDB_CLIENT") ?? "", optional: false)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile(Environment.GetEnvironmentVariable("AZURE_AD") ?? "", optional: false)
                    .AddJsonFile(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "", optional: false)
                    .Build();
            }
            else
            {
                config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .AddUserSecrets("34a2eb48-f55e-4322-8205-5f51e2572770")
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();
            }

            

            // Adds Microsoft Identity platform (Azure AD B2C) support to protect this Api
            // AzureAdB2C is configured to use the react spa
            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //         .AddMicrosoftIdentityWebApi(options =>
            //     {
            //         config.Bind("AzureAdB2C", options);
            //         options.TokenValidationParameters.NameClaimType = "name";
            //     },
            //     options => { config.Bind("AzureAdB2C", options);
            // });

            //OR

            // For local debugging with swagger:
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(options =>
                {
                    config.Bind("AzureAd", options);
                    options.TokenValidationParameters.NameClaimType = "name";
                },
                options => { config.Bind("AzureAd", options);
            });

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
                scopes.Add("https://dominiongamingcompany.onmicrosoft.com/919d8d18-f64a-4a6a-8ee4-91b599eac5e2/gamereview-user", "User access to API");
                scopes.Add("https://dominiongamingcompany.onmicrosoft.com/919d8d18-f64a-4a6a-8ee4-91b599eac5e2/gamereview-admin", "Admin access to DGC");
                c.OperationFilter<SecurityRequirementsOperationFilter>();
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
                            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{config["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"https://login.microsoftonline.com/{config["AzureAd:TenantId"]}/{config["AzureAd:TenantId"]}/v2.0/token"),
                            Scopes = scopes
                        }
                    }
                });
                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);
            });
            var connectionString = config.GetConnectionString("DockerDb");

            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString)
            {
                GuidFormat = MySqlGuidFormat.Char32
            };

            builder.Services.AddDbContext<DockerDbContext>(
                options => options
                .UseLazyLoadingProxies()
                .UseMySql(connectionStringBuilder.ConnectionString, ServerVersion.AutoDetect(connectionStringBuilder.ConnectionString)
           ).EnableDetailedErrors());

            builder.Services.AddHttpClient();
            
            //Services
            builder.Services.AddScoped<IIgdbApiService, IgdbApiService>();
            builder.Services.AddScoped<IPlayRecordService, PlayRecordService>();
            builder.Services.AddScoped<IPlayRecordCommentService, PlayRecordCommentService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ILookupService, LookupService>();
            builder.Services.AddScoped<IUserRelationshipService, UserRelationshipService>();

            //Repositories
            builder.Services.AddTransient(typeof(UnitOfWork));
            builder.Services.AddTransient(typeof(GenericDataAccess<>));
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient(typeof(IGameRepository) ,typeof(GameRepository));

            //Middlewares
            builder.Services.AddTransient<DevelopmentExceptionHandlingMiddleware>();
            builder.Services.AddTransient<ProductionExceptionHandlingMiddleware>();

            builder.Services.AddMapster();

            //Microsoft Graph            
            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                config["AzureAd:TenantId"], config["AzureAd:ClientId"], config["AzureAd:ClientSecret"], options);

            builder.Services.AddSingleton<GraphServiceClient>(sp => {
                return new GraphServiceClient(clientSecretCredential, new[] { "https://graph.microsoft.com/.default" });
            });

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

            if (containerFlag)
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;

                    options.OAuthAppName("Swagger Client");
                    options.OAuthClientId(config["AzureAd:ClientId"]);
                    options.OAuthClientSecret(config["AzureAd:ClientSecret"]);
                    options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                });
            }
            else
            {
                app.UseSwaggerUI(options =>
                {
                    options.OAuthAppName("Swagger Client");
                    options.OAuthClientId(config["AzureAd:ClientId"]);
                    options.OAuthClientSecret(config["AzureAd:ClientSecret"]);
                    options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                });
            }

            app.UseCors(
                options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            );


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                app.UseMiddleware<ProductionExceptionHandlingMiddleware>();
            }
            else
            {
                app.UseMiddleware<DevelopmentExceptionHandlingMiddleware>();
            }


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
