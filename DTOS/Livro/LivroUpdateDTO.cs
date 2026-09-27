using System.ComponentModel.DataAnnotations;

namespace Acervo.DTOS.Livro
{
    public class LivroUpdateDTO
    {
        public string Titulo { get; set; } = string.Empty;
        [MinLength(2, ErrorMessage = "O campo Autor deve ter ao menos 2 caracteres.")]
        public string Autor { get; set; } = string.Empty;
        [Range(1, 2500, ErrorMessage = "O ano de publicação deve estar entre 1 e 2500.")]
        public int AnoPublicacao { get; set; }
    }
}
