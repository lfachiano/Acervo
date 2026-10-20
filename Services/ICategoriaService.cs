using Acervo.DTOS.Categoria;

namespace Acervo.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Listar();
        Task<CategoriaDTO?> BuscarPorId(int id);
        Task<CategoriaDTO> Cadastrar(CategoriaCreateDTO dto);
        Task<CategoriaDTO?> Alterar(int id, CategoriaUpdateDTO dto);
        Task<bool> Excluir(int id);
    }
}
