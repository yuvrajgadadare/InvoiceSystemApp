namespace InvoiceSystemApp.Dtos
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public string? MobileNumber { get; set; }

        public string? City { get; set; }
    }
}
