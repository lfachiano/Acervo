using Acervo.DTOS.Autor;
using Acervo.DTOS.Validation;
using Acervo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acervo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _service;

        public AutorController(IAutorService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var autores = await _service.Listar();
            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _service.BuscarPorId(id);

            if (autor == null)
            {
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "ID não encontrado"
                });
            }

            return Ok(autor);
        }

        [HttpGet("{id}/livros")]
        public async Task<IActionResult> GetLivros(int id)
        {
            var livros = await _service.ListarLivros(id);
            return Ok(livros);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AutorCreateDTO autor)
        {
            try
            {
                var response = await _service.Cadastrar(autor);

                return CreatedAtAction
                    (
                        nameof(GetById),
                        new { id = response.Id },
                        response
                    );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Mensagem = ex.Message,
                });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] AutorUpdateDTO autorAtualizado)
        {
            try
            {
                var autor = await _service.Alterar(id, autorAtualizado);

                if (autor == null)
                {
                    return NotFound(new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Mensagem = "Autor não encontrado"
                    });
                }

                return Ok(new AutorDTO
                {
                    Id = autor.Id,
                    Nome = autor.Nome
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Mensagem = ex.Message,
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var autor = await _service.Excluir(id);

            if (!autor)
                return NotFound();

            return NoContent();
        }
    }
}
