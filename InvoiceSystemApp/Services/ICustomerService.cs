using InvoiceSystemApp.Dtos;

namespace InvoiceSystemApp.Services
{
    public interface ICustomerService
    {
       Task<List<CustomerDTO>> GetCustomers();
        Task<CustomerDTO> GetCustomer(int Id);

    }
}
