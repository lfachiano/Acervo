using Acervo.Data;
using Acervo.DTOS.Categoria;
using Acervo.Models;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context) => _context = context;

        public async Task<List<CategoriaDTO>> Listar()
        {
            return await _context.Categorias
                .AsNoTracking()
                .Select(c => new CategoriaDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
                .ToListAsync();
        }

        public async Task<CategoriaDTO?> BuscarPorId(int id)
        {
            return await _context.Categorias
                .Where(c => c.Id == id)
                .AsNoTracking()
                .Select(c => new CategoriaDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                }).FirstOrDefaultAsync();
        }

        public async Task<CategoriaDTO> Cadastrar(CategoriaCreateDTO dto)
        {
            var existe = await _context.Categorias
                .AnyAsync(c => c.Nome.ToLower().Equals(dto.Nome.ToLower()));

            if (existe)
            {
                throw new InvalidOperationException("Categoria já cadastrada.");
            }

            var categoria = new Categoria
            {
                Nome = dto.Nome
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public async Task<CategoriaDTO?> Alterar(int id, CategoriaUpdateDTO dto)
        {

            var existe = await _context.Categorias.AnyAsync(c => c.Nome.ToLower().Equals(dto.Nome.ToLower()));
            if (existe)
                throw new InvalidOperationException("Categoria já cadastrada");

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
                return null;

            //atualiza os campos
            categoria.Nome = dto.Nome;

            await _context.SaveChangesAsync();

            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public async Task<bool> Excluir(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
