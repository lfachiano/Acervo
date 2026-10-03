using Acervo.Data;
using Acervo.DTOS.Autor;
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

            var query = _context.Livros.AsQueryable();

            if(!string.IsNullOrWhiteSpace(filtro.Titulo))
            {
                query = query.Where(
                    l => l.Titulo.Contains(filtro.Titulo)
                    );
            }

            if (filtro.AnoInicio.HasValue)
            {
                query = query.Where(
                    l => l.AnoPublicacao >= filtro.AnoInicio.Value
                    );
            }

            if (filtro.AnoFim.HasValue)
            {
                query = query.Where(
                    l => l.AnoPublicacao <= filtro.AnoFim.Value
                    );
            }

            return await query
                .Select(l => new LivroResponseDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = new AutorDTO
                    {
                        Id = l.Autor.Id,
                        Nome = l.Autor.Nome
                    },
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
                         Autor = new AutorDTO
                         {
                             Id = l.Autor.Id,
                             Nome = l.Autor.Nome
                         },
                         AnoPublicacao = l.AnoPublicacao
                     }).FirstOrDefaultAsync();
        }
        public async Task<LivroResponseDTO> CadastrarAsync(LivroCreateDTO dto)
        {

            var autorIdExiste = await _context.Autores
                .AnyAsync(a => a.Id == dto.AutorId);

            if (!autorIdExiste)
            {
                throw new InvalidOperationException("Não existe autor com esse ID.");
            }

            /***
             * Verificar se já existe um livro cadastrado com as informações passadas
             * Trata-se de uma regra de negócio que deve ser validada no Service
             * A exceção lançada é tratada pelo controller
             */

            var existe = await _context.Livros
            .AnyAsync(l =>
                l.Titulo == dto.Titulo &&
                l.AutorId == dto.AutorId);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe um livro com este título e autor.");
            }



            var livro = new Livro
            {
                Titulo = dto.Titulo,
                AutorId = dto.AutorId,
                AnoPublicacao = dto.AnoPublicacao
            };

            _context.Livros.Add(livro);

            await _context.SaveChangesAsync();

            /*
            return new LivroResponseDTO
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = new AutorDTO
                {
                    Id = livro.Autor.Id,
                    Nome = livro.Autor.Nome
                },
                AnoPublicacao = livro.AnoPublicacao
            };
            */
            return await BuscarPorIdAsync(livro.Id) ?? throw new InvalidOperationException("Erro ao buscar o livro cadastrado.");

        }

        public async Task<LivroResponseDTO?> AlterarAsync(int id, LivroUpdateDTO dto)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
                return null;


            var autorIdExiste = await _context.Autores
                .AnyAsync(a => a.Id == dto.AutorId);

            if (!autorIdExiste)
            {
                throw new InvalidOperationException("Não existe autor com esse ID.");
            }


            var existe = await _context.Livros
                .AnyAsync(l =>
                    l.Titulo == dto.Titulo &&
                    l.AutorId == dto.AutorId);

            if (existe)
            {
                throw new InvalidOperationException(
                    "Já existe um livro com este título e autor.");
            }


            livro.Titulo = dto.Titulo;
            livro.AutorId = dto.AutorId;
            livro.AnoPublicacao = dto.AnoPublicacao;           

            await _context.SaveChangesAsync();

            return await BuscarPorIdAsync(livro.Id) ?? throw new InvalidOperationException("Erro ao buscar o livro cadastrado.");
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
