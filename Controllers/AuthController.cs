using Acervo.DTOS.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acervo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            if (
                dto.Email == "admin@acervo.com" &&
                dto.Senha == "123456"
            )
            {
                return Ok(
                    "Credenciais válidas. Na próxima etapa será gerado o JWT."
                );
            }

            return Unauthorized(
                new
                {
                    mensagem = "Credenciais inválidas."
                }
            );
        }
    }
}
