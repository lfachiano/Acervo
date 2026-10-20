using System.ComponentModel.DataAnnotations;

namespace Acervo.DTOS.Categoria
{
    public class CategoriaUpdateDTO
    {
        [Required]
        public string Nome { get; set; } = null!;
    }
}
