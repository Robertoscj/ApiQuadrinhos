using ApiQuadrinhos.DTOs;
using ApiQuadrinhos.Entities;
using ApiQuadrinhos.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiQuadrinhos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuadrinhosController : ControllerBase
    {
        private readonly IQuadrinhosRepository _quadrinhoRepository;
        private readonly IMapper _mapper;

        public QuadrinhosController(IQuadrinhosRepository quadrinhoRepository,
            IMapper mapper)
        {
            _quadrinhoRepository = quadrinhoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var quadrinhos = await _quadrinhoRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<QuadrinhosDTO>>(quadrinhos));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var manga = await _quadrinhoRepository.GetByIdAsync(id);

            if (manga is null) return NotFound($"Quadrinho com {id} não encontrado");

            return Ok(_mapper.Map<QuadrinhosDTO>(manga));
        }

        [HttpGet]
        [Route("get-quadrinhos-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetquadrinhosByCategory(int categoryId)
        {
            var quadrinhos = await _quadrinhoRepository.GetQuadrinhosPorCategoriaAsync(categoryId);

            if (!quadrinhos.Any()) return NotFound();

            return Ok(_mapper.Map<IEnumerable<QuadrinhosDTO>>(quadrinhos));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(QuadrinhosDTO mangaDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var manga = _mapper.Map<Quadrinho>(mangaDto);
            await _quadrinhoRepository.AddAsync(manga);

            return Ok(_mapper.Map<QuadrinhosDTO>(manga));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, QuadrinhosDTO mangaDto)
        {
            if (id != mangaDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            await _quadrinhoRepository.UpdateAsync(_mapper.Map<Quadrinho>(mangaDto));

            return Ok(mangaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var quadrinho = await _quadrinhoRepository.GetByIdAsync(id);
            if (quadrinho is null) return NotFound();
            await _quadrinhoRepository.RemoveAsync(quadrinho.Id);
            return Ok();
        }

        [HttpGet]
        [Route("search/{quadrinhoTitulo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<QuadrinhosDTO>>> Search(string mangaTitulo)
        {
            var quadrinhos = await _quadrinhoRepository.SearchAsync(m => m.Titulo.Contains(mangaTitulo));

            if (quadrinhos is null)
                return NotFound("Nenhum mangá foi encontrado");

            return Ok(_mapper.Map<IEnumerable<QuadrinhosDTO>>(quadrinhos));
        }

        [HttpGet]
        [Route("search-quadrinho-with-category/{criterio}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<QuadrinhosCategoriaDTO>>> SearchMangaWithCategory(string criterio)
        {
            var quadrinhos = _mapper.Map<List<Quadrinho>>(await _quadrinhoRepository.LocalizaQuadrinhoComCategoriaAsync(criterio));

            if (!quadrinhos.Any())
                return NotFound("Nenhum quadrinho foi encontrado");

            return Ok(_mapper.Map<IEnumerable<QuadrinhosCategoriaDTO>>(quadrinhos));
        }
    }
}
