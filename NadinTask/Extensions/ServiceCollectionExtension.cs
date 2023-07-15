
using NadinTask.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NadinTask.Domain.Models.Security;

namespace NadinTask.API.Extensions;

public static class ServiceExtension
{
    //public static void ConfigureLoggerService(this IServiceCollection services) =>
    //    services.AddScoped<ILoggerManager, LoggerManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        
        services.AddDbContext<Infrastructure.NadinContext>(
            opts => opts.UseLazyLoadingProxies()   //for lazy loading  --> install nuget ef proxies
            .UseSqlServer(
                environment == Environments.Development ?
                configuration.GetConnectionString("Default"):
                configuration.GetConnectionString("Default")));

        //public static void ConfigureRepositoryManager(this IServiceCollection services)
        //    => services.AddScoped<IRepositoryManager, RepositoryManager>();


    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        // services.AddControllers();
        services.AddControllers(config =>
        {
            config.CacheProfiles.Add("3SecondsCaching", new CacheProfile
            {
                Duration = 3
            });
        });
    }
    public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("JwtConfig");
        var secretKey = jwtConfig["secret"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtConfig["validIssuer"],
            ValidateAudience = true,
            ValidAudience = jwtConfig["validAudience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        };


    });
      
    }
   
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Nadin Task API",
                Version = "v1",
                Description = "Nadin Task API Services.",
            });


            //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },

                    },
                    new string[] {}
                }
            });
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireDigit = false;
                o.Password.RequiredLength = 1;
                o.Password.RequireUppercase = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<Infrastructure.NadinContext>()
            .AddDefaultTokenProviders();

    }





}
