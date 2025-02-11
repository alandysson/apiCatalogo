using ApiCatalogo.Context;
using ApiCatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository: Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context): base(context)
    {       
    }
}