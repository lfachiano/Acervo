using Acervo.Data;
using Acervo.DTOS.Livro;
using Acervo.Models;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Services
{
    public class LivroService : ILivroService
    {

        private readonly AppDbContext _context;

        public LivroService(AppDbContext context) => _context = context;

        public async Task<List<LivroResponseDTO>> ListarAsync(LivroFilterDTO filtro)
        {
            /**
             * Adicionamos a consulta inicial para buscar todos os livros
             * A partir daqui serão adiocionadas as condições de filtro, caso existam
             */
            var query = _context.Livros.AsQueryable();

            /**
             * Verifica se o filtro de título foi fornecido e, se sim, aplicamos a condição de filtro na consulta
             * 
             * Importante notar que foi utilizado o método Contains para realizar uma busca parcial no título do livro, ou seja,
             * caso qualquer parte do título do livro contenha o valor fornecido no filtro, ele será incluído na lista de resultados
             * 
             * Para uma busca específica, você poderia utilizar o operador de igualdade (==) em vez do método Contains, 
             * mas isso limitaria os resultados apenas aos livros cujo título seja exatamente igual ao valor fornecido no filtro
            */
            if (!string.IsNullOrEmpty(filtro.Titulo))
            {
                query = query.Where(l => l.Titulo.Contains(filtro.Titulo));
            }

            /**
             * Verifica se o filtro de ano de publicação foi fornecido e, se sim, aplicamos a condição de filtro na consulta
             */
            if (filtro.AnoPublicacao.HasValue)
            {
                query = query.Where(l => l.AnoPublicacao == filtro.AnoPublicacao.Value);
            }

            /**
             * Verifica se o filtro de ano de início foi fornecido e, se sim, aplicamos a condição de filtro na consulta
             * 
             * Aqui estamos utilizando o operador >= para buscar livros publicados a partir do ano fornecido no filtro
             */
            if (filtro.AnoInicio.HasValue)
            {
                query = query.Where(l => l.AnoPublicacao >= filtro.AnoInicio.Value);
            }

            /**
             * Verifica se o filtro de ano de fim foi fornecido e, se sim, aplicamos a condição de filtro na consulta
             * 
             * Aqui estamos utilizando o operador <= para buscar livros publicados até o ano fornecido no filtro
             * Em conjunto com o filtro de ano de início, isso permite buscar livros publicados dentro de um intervalo de anos específico
             */
            if (filtro.AnoFim.HasValue)
            {
                query = query.Where(l => l.AnoPublicacao <= filtro.AnoFim.Value);
            }

            /**
             * Verifica se o filtro de ordenação foi fornecido e, se sim, aplicamos a ordenação na consulta
             * 
             * Aqui estamos utilizando um switch para determinar qual propriedade do livro será utilizada para ordenar os resultados
             * Caso o valor fornecido para OrdenarPor não seja válido, optamos por não aplicar nenhuma ordenação
             */
            if (!string.IsNullOrEmpty(filtro.OrdenarPor))
            {
                switch (filtro.OrdenarPor.ToLower())
                {
                    case "titulo":
                        query = query.OrderBy(l => l.Titulo);
                        break;
                    case "anopublicacao":
                        query = query.OrderBy(l => l.AnoPublicacao);
                        break;
                    case "autor":
                        query = query.OrderBy(l => l.Autor);
                        break;
                    default:
                        // Caso o valor fornecido para OrdenarPor não seja válido, podemos optar por não aplicar nenhuma ordenação
                        break;
                }
            }


            return await query.Select(l => new LivroResponseDTO
            {
                Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    AnoPublicacao = l.AnoPublicacao
                }).ToListAsync();
        }

        public async Task<LivroResponseDTO?> BuscarPorIdAsync(int id)
        {
            return await _context.Livros
                     .Where(l => l.Id == id)
                     .Select(l => new LivroResponseDTO
                     {
                         Id = l.Id,
                         Titulo = l.Titulo,
                         Autor = l.Autor,
                         AnoPublicacao = l.AnoPublicacao
                     }).FirstOrDefaultAsync();
        }
        public async Task<LivroResponseDTO> CadastrarAsync(LivroCreateDTO dto)
        {
            var livro = new Livro
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                AnoPublicacao = dto.AnoPublicacao
            };

            _context.Livros.Add(livro);

            await _context.SaveChangesAsync();

            return new LivroResponseDTO
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao
            };

        }

        public async Task<LivroResponseDTO?> AlterarAsync(int id, LivroUpdateDTO dto)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
                return null;

            livro.Titulo = dto.Titulo;
            livro.Autor = dto.Autor;
            livro.AnoPublicacao = dto.AnoPublicacao;           

            await _context.SaveChangesAsync();

            return new LivroResponseDTO
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao
            };
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
                return false;

            _context.Livros.Remove(livro);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
