namespace Domain1.Exception
{
    public class NotFoundException
    {
        public class CustomerNotFoundException : Exception
        {
            public CustomerNotFoundException(int customerId)
                : base($"Cliente com ID '{customerId}' não foi encontrado.") { }
        }

        public class ProductNotFoundException : Exception
        {
            public ProductNotFoundException(Guid productId)
                : base($"Produto com ID '{productId}' não foi encontrado.") { }
        }

        public class InvalidBillingException : Exception
        {
            public InvalidBillingException(string message) : base(message) { }
        }

    }
}
