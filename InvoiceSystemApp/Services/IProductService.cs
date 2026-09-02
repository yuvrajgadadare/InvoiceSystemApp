using InvoiceSystemApp.Dtos;

namespace InvoiceSystemApp.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts();
         Task<ProductDTO> GetProduct(int Id);
    }
}
