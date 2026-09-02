using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;

namespace InvoiceSystemApp.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> AddInvoice(InvoiceDTO d);

    }
}
