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

        public async Task<List<LivroResponseDTO>> ListarAsync()
        {
            return await _context.Livros
                .Select(l => new LivroResponseDTO
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
