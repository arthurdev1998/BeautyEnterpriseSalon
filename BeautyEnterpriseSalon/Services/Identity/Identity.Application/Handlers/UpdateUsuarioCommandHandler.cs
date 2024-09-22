// using BuildinBlocks.Core.Messages;
// using Identity.Application.Commands;
// using Identity.Application.Dtos;
// using Identity.Domain;
// using MediatR;

// namespace Identity.Application.Handlers;

// public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, CommandResult<UsuarioDto>>
// {
//     private readonly IConnectionPostgresqlFactory _connectionPostgresqlFactory;

//     public UpdateUsuarioCommandHandler(IConnectionPostgresqlFactory connectionPostgresqlFactory)
//     {
//         _connectionPostgresqlFactory = connectionPostgresqlFactory;
//     }

//     public async Task<CommandResult<UsuarioDto>> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
//     {
//         if (request.Email is null)
//         {
//             CommandResult<UpdateUsuarioCommand>.Fail("Email ou Login nao pode ser nulo");
//         }

//         if (request.CurrentPassword is null)
//         {
//             CommandResult<UpdateUsuarioCommand>.Fail("Senha atual nao pode ser nulo");
//         }

//         if (request.NewPassword is null)
//         {
//             CommandResult<UpdateUsuarioCommand>.Fail("Nova Senha atual nao pode ser nulo");
//         }


//     }

//     private CommandResult<UpdateUsuarioCommand> ValidarCredenciais(UpdateUsuarioCommand credencial)
//     {
//         var loginUsuario = _connectionPostgresqlFactory.GetUsuarioByEmail(credencial.Email);

//         if(loginUsuario is null)
//         {
//             return CommandResult<UpdateUsuarioCommand>.Fail("Usuario nao encontrado");
//         }

//         var 

//     }
// }
