using Acervo.DTOS.Auth;
using Acervo.DTOS.Validation;
using Acervo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acervo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
            => _authService = authService;

        [HttpPost("/login")]
        public async Task<IActionResult> login(LoginDTO dto)
        {
            var response = await _authService.Autenticar(dto);

            if (response == null) 
            {
                return Unauthorized(
                    new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Mensagem = "Credenciais Inválidas"
                    }
                );
            }

            return Ok(response);
        }

        [HttpGet("/me")]
        public IActionResult Me()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var nome = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var perfil = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!int.TryParse(id, out var usuarioId))
            {
                return Unauthorized();
            }

            var usuario = new UsuarioAutenticadoDTO
            {
                Id = usuarioId,
                Nome = nome ?? string.Empty,
                Email = email ?? string.Empty,
                Perfil = perfil ?? string.Empty,
            };

            return Ok(usuario);
        }

    }
}
