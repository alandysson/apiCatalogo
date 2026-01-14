using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using APICatalogo.Pagination;
using ApiCatalogo.Repositories;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        
        public CategoriasController(IUnitOfWork unitOfWork, ILogger<CategoriasController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            _logger.LogInformation("### categorias/produtos ###");
            var categoriasProdutos = _unitOfWork.CategoriaRepository.GetCategoriasProdutos();
            
            return Ok(categoriasProdutos);
        }
        private ActionResult<IEnumerable<ProdutoDTO>> ObterCategorias(PagedList<Categoria> categorias)
        {
            var metadata = new
            {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.CurrentPage,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var categoriasDto = categorias.ToCategoriasDTOList();
            
            return Ok(categoriasDto);
        }

        [ServiceFilter(typeof(ApiLoggingFilter))]
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            try
            {
                var categorias = _unitOfWork.CategoriaRepository.GetAll();

                if (categorias is null)
                {
                    return NotFound("Não existem categorias");
                }
                
                var categoriasDto = categorias.ToCategoriasDTOList();
                return Ok(categoriasDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao carregar Categorias");
            }

        }
        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = _unitOfWork.CategoriaRepository.GetCategorias(categoriasParameters);

            return ObterCategorias(categorias);
        }


        [HttpGet("filter/nome/pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetCategoriasFiltroNome(
            [FromQuery] CategoriasFiltroNome categoriasParams)
        {
            var categorias = _unitOfWork.CategoriaRepository.GetCategoriasFiltroNome(categoriasParams);

            return ObterCategorias(categorias);
        }

        
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            var categoriaDto = categoria.ToCategoriaDTO();
            return Ok(categoriaDto);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                return BadRequest();
            }

            var categoria = categoriaDto.ToCategoria();
            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, novaCategoriaDto);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }
            var categoria = categoriaDto.ToCategoria();
            _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();
            
            var categoriaAtualizadaDto = categoria.ToCategoriaDTO();
            return Ok(categoriaAtualizadaDto);
        }
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }
            
            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();
            
            var categoriaExcluidaDto = categoria.ToCategoriaDTO();
            return Ok(categoriaExcluidaDto);
        }
    }
}