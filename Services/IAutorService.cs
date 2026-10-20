using Acervo.DTOS.Autor;
using Acervo.DTOS.Categoria;
using Acervo.DTOS.Livro;

namespace Acervo.Services
{
    public interface IAutorService
    {
        Task<List<AutorDTO>> Listar();
        Task<AutorDTO?> BuscarPorId(int id);
        Task<List<LivroResponseDTO>> ListarLivros(int idAutor);
        Task<AutorDTO> Cadastrar(AutorCreateDTO dto);
        Task<AutorDTO?> Alterar(int id, AutorUpdateDTO dto);
        Task<bool> Excluir(int id);
    }
}
