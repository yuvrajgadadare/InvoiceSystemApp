using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;

namespace InvoiceSystemApp.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> AddInvoice(InvoiceDTO d);
        Task<List<InvoiceModelDTO>> GetInvoices();
        Task<InvoiceModelDTO> GetInvoice(int Id);
        Task<PaymentFormDTO> SubmitPayment(PaymentFormDTO p);
        Task<List<InvoiceProductDTO>> GetInvoiceWiseProducts(int InvoiceId);
        Task<List<PaymentFormDTO>> GetInvoiceWisePayments(int InvoiceId);
    }
}
