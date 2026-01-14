using ApiCatalogo.Context;
using ApiCatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {        
    }
    
    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();
        
        var categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias, 
            categoriasParameters.PageNumber, categoriasParameters.PageSize);
        
        return categoriasOrdenados;
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams)
    {
        var categorias = GetAll().AsQueryable();
        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome, StringComparison.OrdinalIgnoreCase));
        }
    
        var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
    
        return categoriasFiltradas;
    }
    public IEnumerable<Categoria> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
    }   
}