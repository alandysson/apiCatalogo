using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCatalogo.Validations;

namespace ApiCatalogo.Models;
[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre 5 e 80 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }
    [Required]
    [StringLength(80, ErrorMessage = "A descrição deve ter entre 5 e 80 caracteres", MinimumLength = 5)]
    public string? Descricao { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(1, 100000, ErrorMessage = "O preço deve ser entre 1 e 10000")]
    public decimal Preco { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}