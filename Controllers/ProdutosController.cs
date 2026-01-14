using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Models;
using APICatalogo.Pagination;
using ApiCatalogo.Repositories;
using APICatalogo.Repositories;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos =  await _unitOfWork.ProdutoRepository.GetAllAsync();
            if (produtos.Count() == 0)
            {
                return NotFound("Produtos não encontrados");
            }

            var produtosDto = produtos.ToProdutosDtoList();
            return Ok(produtosDto);
        }
        // Métodos
        private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(PagedList<Produto> produtos)
        {
            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var produtosDto = produtos.ToProdutosDtoList();
            return Ok(produtosDto);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosAsync(produtosParameters);

            return ObterProdutos(produtos);
        }
        
        [HttpGet("filter/preco/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco
            produtosFilterParameters)
        {
            var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFilterParameters);
            return ObterProdutos(produtos);
        }
        
        [HttpGet("saudacao/{nome}")] // testando o service
        public ActionResult<string> GetSaudacaoService(IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        } 
        
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDto = produto.ToProdutoDTO();
            return Ok(produtoDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
            {
                return BadRequest();
            }

            var produto = produtoDto.ToProduto();
            var produtoCriado = _unitOfWork.ProdutoRepository.Create(produto);
            await _unitOfWork.CommitAsync();
            
            var novoProduto = produtoCriado.ToProdutoDTO();
            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = produtoDto.ToProduto();
            _unitOfWork.ProdutoRepository.Update(produto);
            await _unitOfWork.CommitAsync();
            
            var produtoAtualizado = produto.ToProdutoDTO();
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado!");
            }            
            
            _unitOfWork.ProdutoRepository.Delete(produto);
            await _unitOfWork.CommitAsync();
            
            var produtoRemovido = produto.ToProdutoDTO();
            return Ok(produtoRemovido);
        }
    }
}
