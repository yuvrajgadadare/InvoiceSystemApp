namespace InvoiceSystemApp.Dtos
{
    public class InvoiceModelDTO
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public DateOnly InvoiceDate { get; set; }

        public double? InvoiceAmount { get; set; }
        public double PaidAmount { get; set; }
        public double RemainingAmount { get; set; }
        public  string Status{ get; set; }

    }
}
