using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DAL;

namespace WebApp.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPetRegistrationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbContext>((sp) =>
            {
                var option = PetTrackingContextOptionFactoryMethod(configuration);
                return new PetTrackingContext(option);
            });
        }

        private static DbContextOptions<PetTrackingContext> PetTrackingContextOptionFactoryMethod(IConfiguration configuration) =>
            new DbContextOptionsBuilder<PetTrackingContext>().UseSqlServer(configuration.GetConnectionString("PetTrackingDatabase")).Options;
    }
}