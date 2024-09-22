using BuildinBlocks.Core.Messages;
using Identity.Application.Commands;
using Identity.Application.Dtos;
using Identity.Application.Handlers;
using Identity.Domain;
using Identity.Infra.Data.Repositories;
using MediatR;

namespace Identity.Api.Configurations;

public static class ServicesConfiguration
{
    public static void AddServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<InsertUsuarioCommand, CommandResult<UsuarioDto>>, InsertUsuarioCommandHandler>();
        // services.AddScoped<IConnectionPostgresqlFactory, ConnectionPostgresqlFactory>();
    }
}
