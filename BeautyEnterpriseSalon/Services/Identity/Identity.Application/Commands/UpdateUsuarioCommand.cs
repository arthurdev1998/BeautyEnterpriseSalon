using BuildinBlocks.Core.Messages;
using Identity.Application.Dtos;

namespace Identity.Application.Commands;

public class UpdateUsuarioCommand : Command<string, CommandResult<UsuarioDto>>
{
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}