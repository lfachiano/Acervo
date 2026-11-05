using Acervo.DTOS.Auth;
using Acervo.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Acervo.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) 
            => _configuration = configuration;

        public TokenResponseDTO GerarToken(Usuario usuario)
        {
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("A Chave JWT não foi configurada.");

            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("O Issuer JWT não foi configurado.");

            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("A Audience JWT não foi configurada.");

            var expirationMinutes = _configuration.GetValue<int>("Jwt:ExpirationMinutes");
            var expiracao = DateTime.UtcNow.AddMinutes(expirationMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiracao,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseDTO
            {
                Token = tokenString,
                Expiracao = expiracao,
                Nome = usuario.Nome,
                Perfil = usuario.Perfil,
            };
        }
    }
}
