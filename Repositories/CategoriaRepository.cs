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
    
    public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters)
    {
        var categorias = await GetAllAsync();
        var categoriasOrdenadas = categorias.OrderBy(c => c.CategoriaId).AsQueryable();
        
        var categoriasResponse = PagedList<Categoria>.ToPagedList(categoriasOrdenadas, 
            categoriasParameters.PageNumber, categoriasParameters.PageSize);
        
        return categoriasResponse;
    }

    public async Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasParams)
    {
        var categorias = await GetAllAsync();
        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome, StringComparison.OrdinalIgnoreCase));
        }
    
        var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias.AsQueryable(), categoriasParams.PageNumber, categoriasParams.PageSize);
    
        return categoriasFiltradas;
    }
    public async Task<IEnumerable<Categoria>> GetCategoriasProdutosAsync()
    {
        return await _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToListAsync();
    }   
}