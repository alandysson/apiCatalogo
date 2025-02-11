using ApiCatalogo.Context;
using ApiCatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {        
    }
    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
    }   
}