namespace Domain.Entities
{
    public class Product
    {
        public required int Id { get; set; }
        public required string NomeDoProduto { get; set; }

        public ICollection<BillingLine> BillingLines { get; set; }
    }
}
