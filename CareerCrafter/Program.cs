using CareerCrafterClassLib;
using CareerCrafterClassLib.Data;
using CareerCrafterClassLib.Interface;
using CareerCrafterClassLib.Repository;
using JWT.Logic;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace CareerCrafter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize log4net
            ConfigureLog4Net();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            ConfigureServices(builder);

            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline
            ConfigureMiddleware(app);

            // Run the application
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(); // Add MVC controllers

            // Register the AppDbContext with the dependency injection container
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("CareerCrafter")
                )
            );

            // Register repositories and services
            RegisterServices(builder.Services);

            // Configure Swagger/OpenAPI
            ConfigureSwagger(builder.Services);

            // Configure JWT authentication
            ConfigureJwtAuthentication(builder.Services, builder.Configuration);

            // Add CORS policy
            ConfigureCors(builder.Services);
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            // Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CareerCrafter API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IEmployerRepository, EmployerRepository>();
            services.AddScoped<IJobPostingsRepository, JobPostingsRepository>();
            services.AddScoped<IResumeRepository, ResumeRepository>();
            services.AddScoped<IJobSeekerService, JobSeekerService>();
            services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();
            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IApplicationStatusRepository, ApplicationStatusRepository>();
            services.AddScoped<IApplicationStatusService, ApplicationStatusService>();

            // Register Token generation service
            services.AddSingleton<TokenGeneration>();
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CareerCrafter API", Version = "v1" });

                // JWT Authorization header setup
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static void ConfigureLog4Net()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        private static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetValue<string>("ApiSettings:Secret");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true // Ensure the token's lifetime is validated
                };
            });

            // Add authorization service
            services.AddAuthorization();
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Specify your frontend URL
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }
    }
}
