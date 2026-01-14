using ApiCatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;

namespace ApiCatalogo.Repositories;

public interface IProdutoRepository: IRepository<Produto>
{
    Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParams);
    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
}