using System.Text;
using Identity.Api.Configurations.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Configurations;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("AppSettings");
        services.Configure<Settings>(settings);

        services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));

        var appSettings = settings.Get<Settings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret!);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // Request para JWT
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Response para JWT
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidoEm,
                ValidIssuer = appSettings.Emisor
            };
        });

        return services;
    }
}
