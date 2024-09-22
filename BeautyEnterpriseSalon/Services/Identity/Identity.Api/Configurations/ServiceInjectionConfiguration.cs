using Identity.Domain;
using Identity.Domain.Options;
using Identity.Infra.Data;
using Identity.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.Api.Configurations;

public static class ServiceInjectionConfiguration
{
    public static void AddServicesInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));

        services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DbConnection") ?? 
                throw new InvalidOperationException("Connection string not found.")));

        services.AddScoped<IConnectionPostgresqlFactory>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<ConnectionSettings>>().Value;

            return new ConnectionPostgresqlFactory(options.DbConnection!);
        });

    }
}