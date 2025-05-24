using BackEnd.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Extensions
{
    public static class ServicesExtentions
    {
        public static void ConfigurePostgresContext(this IServiceCollection services,IConfiguration configuration)
        {
            // Register DbContext with PostgreSQL
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}