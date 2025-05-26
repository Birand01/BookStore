using BackEnd.Repositories;
using BackEnd.Repositories.Contracts;
using BackEnd.Repositories.EFCore;
using BackEnd.Services.Contracts;
using BackEnd.Services.Managers;
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
    }
}