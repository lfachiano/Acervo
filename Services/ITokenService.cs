using Acervo.DTOS.Auth;
using Acervo.Models;

namespace Acervo.Services
{
    public interface ITokenService
    {
        TokenResponseDTO GerarToken(Usuario usuario);
    }
}
