using Acervo.DTOS.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acervo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            if (
                dto.Email == "admin@outromail.com"
                && dto.Senha == "abc123"
            )
            {
                //Após validar credenciais,
                //deve ser gerado o Token
                return Ok("Credenciais válidas.");

            }

            return Unauthorized(
                new { 
                    mensagem = "Credenciais inválidas" 
                });
        }

    }
}
