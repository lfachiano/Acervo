using Acervo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Acervo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private static List<Livro> _livros = new List<Livro>()
        {
            new Livro
            {
                Id = 1,
                Titulo = "O Senhor dos Aneis",
                Autor = "J.R.R. Tokien",
                AnoPublicacao = 1954
            },
            new Livro
            {
                Id = 2,
                Titulo = "1984",
                Autor = "George Orwell",
                AnoPublicacao = 1949
            },
             new Livro
            {
                Id = 3,
                Titulo = "O Pequeno Prícipe",
                Autor = "Antoine de Saint-Exupery",
                AnoPublicacao = 1943
            }
        };


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_livros);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var livro = _livros.FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        [HttpGet("ano/{ano}")]
        public IActionResult GetByAnoPublicacao(int ano)
        {
            var livros = _livros
                .Where(l => l.AnoPublicacao == ano)
                .ToList();

            if (livros.Count == 0)
            {
                return NotFound();
            }

            return Ok(livros);
        }


        [HttpPost]
        public IActionResult Post([FromBody]Livro livro)
        {
            var id = _livros.Any() ?
                _livros.Max(l => l.Id) + 1 :
                1;

            livro.Id = id;

            _livros.Add(livro);

            return CreatedAtAction
                (
                    nameof(GetById),
                    new { id = livro.Id },
                    livro
                );
        }





    }
}
