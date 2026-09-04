namespace InvoiceSystemApp.Dtos
{
    public class InvoiceProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public double Rate { get; set; }

        public double Gst { get; set; }
        public double TotalAmount { get; set; }
        public int Quantity { get; set; }
    }
}
