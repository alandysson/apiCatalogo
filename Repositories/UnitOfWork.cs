using ApiCatalogo.Context;

namespace ApiCatalogo.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private IProdutoRepository? _produtoRep;
    private ICategoriaRepository? _categoriaRep;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRep = _produtoRep ?? new ProdutoRepository(_context);
        }
    }
    
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRep = _categoriaRep ?? new CategoriaRepository(_context);
        }
    }
    
    public async Task CommitAsync()
    {
       await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}