using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemApp.Services
{
    public class ProductService : IProductService
    {
        InvoiceSystemDbContext db;
        public ProductService(InvoiceSystemDbContext db)
        {
            this.db = db;
        }
        public async Task<ProductDTO> GetProduct(int Id)
        {
            Tblproduct p = await db.Tblproducts.FindAsync(Id);
            return new ProductDTO
            {
                ProductId = p.ProductId,
                Gst = p.Gst,
                ProductName = p.ProductName,
                Rate = p.Rate,
                StockQuantity = p.StockQuantity
            };
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            List<ProductDTO> lst = new List<ProductDTO>();
            foreach(Tblproduct p in await db.Tblproducts.ToListAsync())
            {
                lst.Add(new ProductDTO { 
                 ProductId=p.ProductId,
                  Gst=p.Gst,
                   ProductName=p.ProductName,
                    Rate=p.Rate,
                     StockQuantity=p.StockQuantity
                });
            }
            return lst;
        }
    }
}
