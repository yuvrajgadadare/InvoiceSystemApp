namespace InvoiceSystemApp.Dtos
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public double Rate { get; set; }

        public double Gst { get; set; }

        public double StockQuantity { get; set; }
    }
}
