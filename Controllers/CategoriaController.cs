using Acervo.DTOS.Categoria;
using Acervo.DTOS.Livro;
using Acervo.DTOS.Validation;
using Acervo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acervo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _service.Listar();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _service.BuscarPorId(id);

            if (categoria == null)
            {
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "ID não encontrado"
                });
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriaCreateDTO categoria)
        {
            try
            {
                var response = await _service.Cadastrar(categoria);

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
        public async Task<IActionResult> Alterar(int id, [FromBody] CategoriaUpdateDTO categoriaAtualizada)
        {
            try
            {
                var categoria = await _service.Alterar(id, categoriaAtualizada);

                if (categoria == null)
                {
                    return NotFound(new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Mensagem = "Categoria não encontrada"
                    });
                }

                return Ok(new CategoriaDTO
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome
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
            var categoria = await _service.Excluir(id);

            if (!categoria)
                return NotFound();

            return NoContent();
        }

    }
}
