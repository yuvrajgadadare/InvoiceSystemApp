namespace InvoiceSystemApp.Dtos
{
    public class InvoiceDTO
    {
      
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }

        public DateOnly InvoiceDate { get; set; }

        public double? InvoiceAmount { get; set; }

        public List<InvoiceProductDTO> InvoiceProducts { get; set; }

    }
}
