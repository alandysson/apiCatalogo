using System.ComponentModel.DataAnnotations;
using ApiCatalogo.Validations;

namespace ApiCatalogo.DTOs;

public class ProdutoDTO
{
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 5 e 80 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required]
    [StringLength(80, ErrorMessage = "A descrição deve ter entre 5 e 80 caracteres", MinimumLength = 5)]
    public string? Descricao { get; set; }
    [Required]
    [Range(1, 100000, ErrorMessage = "O preço deve ser entre 1 e 10000")]
    public decimal Preco { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public int CategoriaId { get; set; }
}