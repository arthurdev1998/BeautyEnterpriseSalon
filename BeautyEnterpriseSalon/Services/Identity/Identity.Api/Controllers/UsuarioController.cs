using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Identity.Api.Configurations.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Controllers;

[Route("/api")]
public class UsuarioController : ControllerBase
{
    private readonly IOptions<Settings> _options;
    public UsuarioController(IOptions<Settings> options)
    {
        _options = options;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> GetToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _options.Value.Emisor,
            Audience = _options.Value.ValidoEm,
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);
        return Ok(encodedToken);
    }
}
