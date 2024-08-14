using Identity.Application.Commands;
using MediatR;

namespace Identity.Application.Handlers;

public class InsertUsuarioCommandHandler : IRequestHandler<InsertUsuarioCommand, bool>
{
    public Task<bool> Handle(InsertUsuarioCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}