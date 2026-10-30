using System.ComponentModel.DataAnnotations;

namespace Acervo.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        public string Perfil {  get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
    }
}
