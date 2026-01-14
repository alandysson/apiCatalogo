using ApiCatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;

namespace ApiCatalogo.Repositories;

public interface IProdutoRepository: IRepository<Produto>
{
    PagedList<Produto> GetProdutos(ProdutosParameters produtosParams);
    PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams);
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}