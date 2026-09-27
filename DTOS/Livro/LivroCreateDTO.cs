

using System.ComponentModel.DataAnnotations;

namespace Acervo.DTOS.Livro
{
    public class LivroCreateDTO
    {
        [Required(ErrorMessage = "O campo Titulo é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        [MinLength(2, ErrorMessage = "O campo Autor deve ter ao menos 2 caracteres.")]
        public string Autor { get; set; } = string.Empty;
        [Range(1, 2500, ErrorMessage = "O ano de publicação deve estar entre 1 e 2500.")]
        public int AnoPublicacao { get; set; }
    }
}
