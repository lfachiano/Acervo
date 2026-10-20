namespace Acervo.DTOS.Auth
{
    public class TokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiracao { get; set; }
    }
}
