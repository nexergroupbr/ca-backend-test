namespace Domain.Dtos;

public record BillingViewModel(Guid Id, string InvoiceNumber, Guid CustomerId, string CustomerName, DateOnly Date, DateOnly DueDate, decimal Total, string Currency, IList<BillingLineViewModel> Lines);

public record BillingLineViewModel(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice, decimal SubTotal);
