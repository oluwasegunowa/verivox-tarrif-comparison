using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VerivoxTask.Infrastructure.Persistence.Repository;

namespace VerivoxTask.Infrastructure.Persistence.Extensions
{
    public static class ServiceExtensions
    {

        public static void AddRepository(this IServiceCollection services, string connectionStrng)
        {
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionStrng);
            });
        }
    }
}
