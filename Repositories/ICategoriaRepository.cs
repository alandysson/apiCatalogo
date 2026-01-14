using ApiCatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;

namespace ApiCatalogo.Repositories;

public interface ICategoriaRepository: IRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters);
    Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParameters);
    Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync();
}