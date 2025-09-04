namespace Domain.Entities
{
    public class BillingLine
    {
        public Billing Billing { get; set; }
        public Product Product { get; set; }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BillingId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
