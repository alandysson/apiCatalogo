using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using APICatalogo.Repositories;
using ApiCatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos =  _unitOfWork.ProdutoRepository.GetAll();
            if (produtos.Count() == 0)
            {
                return NotFound("Produtos não encontrados");
            }

            var produtosDto = produtos.ToProdutosDtoList();
            return Ok(produtosDto);
        }

        [HttpGet("saudacao/{nome}")] // testando o service
        public ActionResult<string> GetSaudacaoService(IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        } 
        
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDto = produto.ToProdutoDTO();
            return Ok(produtoDto);
        }

        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
            {
                return BadRequest();
            }

            var produto = produtoDto.ToProduto();
            var produtoCriado = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();
            
            var novoProduto = produtoCriado.ToProdutoDTO();
            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = produtoDto.ToProduto();
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();
            
            var produtoAtualizado = produto.ToProdutoDTO();
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado!");
            }            
            
            _unitOfWork.ProdutoRepository.Delete(produto);
            _unitOfWork.Commit();
            
            var produtoRemovido = produto.ToProdutoDTO();
            return Ok(produtoRemovido);
        }
    }
}
