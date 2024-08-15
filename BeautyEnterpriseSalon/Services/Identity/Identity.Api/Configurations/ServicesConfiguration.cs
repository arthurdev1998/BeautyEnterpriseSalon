using BuildinBlocks.Core.Messages;
using Identity.Application.Commands;
using Identity.Application.Handlers;
using MediatR;

namespace Identity.Api.Configurations;

public static class ServicesConfiguration
{
    public static void AddServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<InsertUsuarioCommand, CommandResult<InsertUsuarioCommand>>, InsertUsuarioCommandHandler>();
    }
}
