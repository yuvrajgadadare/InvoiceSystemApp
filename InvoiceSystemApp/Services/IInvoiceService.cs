using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;

namespace InvoiceSystemApp.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> AddInvoice(InvoiceDTO d);
        Task<List<InvoiceModelDTO>> GetInvoices();
        Task<InvoiceModelDTO> GetInvoice(int Id);
    }
}
