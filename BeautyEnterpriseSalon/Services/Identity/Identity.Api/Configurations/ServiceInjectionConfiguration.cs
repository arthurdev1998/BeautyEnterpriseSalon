using Identity.Api.Configurations.Options;
using Identity.Application.DbConnection;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Identity.Api.Configurations;

public static class ServiceInjectionConfiguration
{
    public static void AddServicesInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));

        services.AddScoped<IConnectionPostgresqlFactory>(provider =>
        {
        var options = provider.GetRequiredService<IOptions<ConnectionSettings>>().Value;

            return new ConnectionPostgresqlFactory(options.DbConnection!);
        });
    }
}
