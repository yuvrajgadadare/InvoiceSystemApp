namespace InvoiceSystemApp.Dtos
{
    public class PaymentFormDTO
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }

        public double PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }

        public string PaymentMode { get; set; } = null!;

        public string? PaymentDescription { get; set; }
    }
}
