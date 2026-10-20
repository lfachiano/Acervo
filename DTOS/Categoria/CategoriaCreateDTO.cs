using System.ComponentModel.DataAnnotations;

namespace Acervo.DTOS.Categoria
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = null!;
    }
}
