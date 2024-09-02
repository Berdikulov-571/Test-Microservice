using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Registration.Application.Abstraction;
using Registration.Inftastructure.Persistance;
using System.Text.Json.Serialization;

namespace Registration.Inftastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInftastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Docker");
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.IgnoreCycles);

            return services;
        }
    }
}