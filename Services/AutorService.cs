using Acervo.Data;
using Acervo.DTOS.Autor;
using Acervo.DTOS.Categoria;
using Acervo.DTOS.Livro;
using Acervo.Models;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Services
{
    public class AutorService : IAutorService
    {
        private readonly AppDbContext _context;

        public AutorService(AppDbContext context) => _context = context;

        public async Task<List<AutorDTO>> Listar()
        {
            return await _context.Autores
                .AsNoTracking()
                .Select(a => new AutorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                })
                .ToListAsync();
        }

        public async Task<AutorDTO?> BuscarPorId(int id)
        {
            return await _context.Autores
                .Where(a => a.Id == id)
                .AsNoTracking()
                .Select(a => new AutorDTO
                {
                    Id = a.Id,
                    Nome = a.Nome
                }).FirstOrDefaultAsync();
        }

        public async Task<List<LivroResponseDTO>> ListarLivros(int idAutor)
        {
            return await _context.Livros
                .AsNoTracking()
                .Where(l => l.AutorId == idAutor)
                .Select(l => new LivroResponseDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = new AutorDTO
                    {
                        Id = l.Autor.Id,
                        Nome = l.Autor.Nome
                    },
                    AnoPublicacao = l.AnoPublicacao,
                    Categorias = l.Categorias
                        .Select(c => new CategoriaDTO
                        {
                            Id = c.Id,
                            Nome = c.Nome
                        })
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<AutorDTO> Cadastrar(AutorCreateDTO dto)
        {
            var existe = await _context.Autores
                .AnyAsync(a => a.Nome.ToLower().Equals(dto.Nome.ToLower()));

            if (existe)
            {
                throw new InvalidOperationException("Autor já cadastrada.");
            }

            var autor = new Autor
            {
                Nome = dto.Nome
            };

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return new AutorDTO
            {
                Id = autor.Id,
                Nome = autor.Nome
            };
        }

        public async Task<AutorDTO?> Alterar(int id, AutorUpdateDTO dto)
        {

            var existe = await _context.Autores.AnyAsync(a => a.Nome.ToLower().Equals(dto.Nome.ToLower()));
            if (existe)
                throw new InvalidOperationException("Autor já cadastrado");

            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
                return null;

            //atualiza os campos
            autor.Nome = dto.Nome;

            await _context.SaveChangesAsync();

            return new AutorDTO
            {
                Id = autor.Id,
                Nome = autor.Nome
            };
        }

        public async Task<bool> Excluir(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
                return false;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
