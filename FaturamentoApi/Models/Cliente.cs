using System.ComponentModel.DataAnnotations;

namespace FaturamentoApi.Models
{
	public class Cliente
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Nome { get; set; }

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }

		[Required]
		[StringLength(200)]
		public string Endereco { get; set; }

		public int ProdutoId { get; set; }

		public ICollection<Produto>? Produtos { get; set; }
	}
}