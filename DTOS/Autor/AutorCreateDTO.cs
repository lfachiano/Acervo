using System.ComponentModel.DataAnnotations;

namespace Acervo.DTOS.Autor
{
    public class AutorCreateDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
    }
}
