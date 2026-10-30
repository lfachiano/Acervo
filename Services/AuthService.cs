using Acervo.Data;
using Acervo.DTOS.Auth;
using Acervo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            AppDbContext context,
            IPasswordHasher<Usuario> passwordHasher,
            ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDTO?> Autenticar(LoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null)
                return null;

            if (!usuario.Ativo)
                return null;

            var result = _passwordHasher
                .VerifyHashedPassword(usuario, usuario.SenhaHash, dto.Senha);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return _tokenService.GerarToken(usuario);
        }
    }
}
