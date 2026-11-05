using Acervo.Models;
using Acervo.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Acervo.Data
{
    public static class DbSeeder
    {

        public static async Task SeedAsync(
            AppDbContext context,
            IPasswordHasher<Usuario> hasher)
        {
            var adminExiste = await context.Usuarios
                .AnyAsync(u => u.Email == "admin@acervo.com");

            if (!adminExiste)
            {
                var admin = new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@acervo.com",
                    Perfil = Perfis.Administrador,
                    Ativo = true
                };

                admin.SenhaHash = hasher
                    .HashPassword(admin, "Admin@123");

                context.Usuarios.Add(admin);
            }

            var usuarioExiste = await context.Usuarios
                .AnyAsync(u => u.Email == "usuario@acervo.com");

            if (!usuarioExiste)
            {
                var usuario = new Usuario
                {
                    Nome = "Usuário Comum",
                    Email = "usuario@acervo.com",
                    Perfil = Perfis.Usuario,
                    Ativo = true
                };

                usuario.SenhaHash = hasher
                    .HashPassword(usuario, "Usuario@123");

                context.Usuarios.Add(usuario);
            }

            await context.SaveChangesAsync();
        }
    }
}
