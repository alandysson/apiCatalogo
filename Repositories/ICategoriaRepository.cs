using ApiCatalogo.Models;
using APICatalogo.Repositories;

namespace ApiCatalogo.Repositories;

public interface ICategoriaRepository: IRepository<Categoria>
{
    IEnumerable<Categoria> GetCategoriasProdutos();
}