using Microsoft.OpenApi.Models;

namespace Identity.Api.Configurations;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint("v1/swagger.json", "v1");
        });
    }

    public static void AddSwaggerGenConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NerdStore Enterprise IdentityApi",
                    Description = "Curso",
                    Contact = new OpenApiContact() { Name = "arthurdev1998@gmail.com" },
                });

                x.AddSecurityDefinition(name: "Bearer",
                                             securityScheme: new OpenApiSecurityScheme
                                             {
                                                 Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                                                 Name = "Authorization",
                                                 Scheme = "Bearer",
                                                 BearerFormat = "JWT",
                                                 In = ParameterLocation.Header,
                                                 Type = SecuritySchemeType.ApiKey
                                             });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
    }
}
