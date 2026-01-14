using ApiCatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;

namespace ApiCatalogo.Repositories;

public interface ICategoriaRepository: IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);
    PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParameters);
    IEnumerable<Categoria> GetCategoriasProdutos();
}