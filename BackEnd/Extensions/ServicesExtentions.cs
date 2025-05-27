using AspNetCoreRateLimit;
using BackEnd.Repositories;
using BackEnd.Repositories.Contracts;
using BackEnd.Repositories.EFCore;
using BackEnd.Services.Contracts;
using BackEnd.Services.Managers;
using Marvin.Cache.Headers;
using Microsoft.EntityFrameworkCore;


namespace BackEnd.Extensions
{
    public static class ServicesExtentions
    {
        public static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with PostgreSQL
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddSingleton<ILoggerService, LoggerManager>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options=>
            {
                options.AddPolicy("CorsPolicy",builder=>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });
        }
        public static void ConfigureResponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expirationOptions=>
            {
                expirationOptions.MaxAge=90;
                expirationOptions.CacheLocation=CacheLocation.Public;

            },
            validationOptions=>
            {
                validationOptions.MustRevalidate=false;
            });
        }
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            var rateLimitRules=new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint="*", // Apply rate limit to all endpoints
                    Limit=3, // Limit to 3 requests per second
                    Period="1m" // Per second
                }
            };
            services.Configure<IpRateLimitOptions>(options=>
            {
                options.GeneralRules=rateLimitRules;
            });
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        }
    }
}