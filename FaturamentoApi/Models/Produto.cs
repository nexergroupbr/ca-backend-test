using System.ComponentModel.DataAnnotations;
namespace FaturamentoApi.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeProduto { get; set; }
    }
}