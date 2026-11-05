using Acervo.DTOS.Auth;

namespace Acervo.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDTO?> Autenticar(LoginDTO dto);
    }
}
