using ApiCatalogo.Models;

namespace ApiCatalogo.DTOs.Mappings;

public static class ProdutoDTOMappingExtensions
{
    public static ProdutoDTO ToProdutoDTO(this Produto produto)
    {
        if (produto == null) return null;

        return new ProdutoDTO()
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ImagemUrl = produto.ImagemUrl,
            CategoriaId = produto.CategoriaId,
        };
    }
    
    public static Produto ToProduto(this ProdutoDTO produtoDto)
    {
        if (produtoDto == null) return null;

        return new Produto()
        {
            Nome = produtoDto.Nome,
            Descricao = produtoDto.Descricao,
            Preco = produtoDto.Preco,
            ImagemUrl = produtoDto.ImagemUrl,
            CategoriaId = produtoDto.CategoriaId,
        };
    }
    
    public static IEnumerable<ProdutoDTO> ToProdutosDtoList(this IEnumerable<Produto> produtos)
    {
        if (produtos is null || !produtos.Any())
        {
            return new List<ProdutoDTO>();
        }

        return produtos.Select(produto => new ProdutoDTO()
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ImagemUrl = produto.ImagemUrl,
            CategoriaId = produto.CategoriaId,
        }).ToList();
    }
}