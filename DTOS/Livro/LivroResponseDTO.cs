using Acervo.DTOS.Autor;
<<<<<<< HEAD
using Acervo.DTOS.Categoria;
=======
>>>>>>> 4ab22e388cff521e3931eba276f896d57250ee06

namespace Acervo.DTOS.Livro
{
    public class LivroResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public AutorDTO? Autor {  get; set; }
        public int AnoPublicacao { get; set; }
<<<<<<< HEAD
        public List<CategoriaDTO> Categorias { get; set; } = null!; 
=======
>>>>>>> 4ab22e388cff521e3931eba276f896d57250ee06
    }
}
