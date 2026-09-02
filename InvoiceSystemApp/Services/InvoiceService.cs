using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;

namespace InvoiceSystemApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        InvoiceSystemDbContext db;
        public InvoiceService(InvoiceSystemDbContext db)
        {
            this.db = db;
        }
        public async Task<InvoiceDTO> AddInvoice(InvoiceDTO d)
        {
            List<TblinvoiceProduct> products = new List<TblinvoiceProduct>();
            foreach(InvoiceProductDTO p in d.InvoiceProducts)
            {
                products.Add(new TblinvoiceProduct()
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                });
 
            }
            TblinvoiceDetail dr = new TblinvoiceDetail
            {
                InvoiceDate = d.InvoiceDate,
                CustomerId = d.CustomerId,
                InvoiceAmount = d.InvoiceAmount,
                TblinvoiceProducts = products
            };
            await db.TblinvoiceDetails.AddAsync(dr);
            await db.SaveChangesAsync();
            InvoiceDTO pd = new InvoiceDTO()
            {
                InvoiceId = dr.InvoiceId,
                CustomerId = dr.CustomerId,
                InvoiceAmount = dr.InvoiceAmount,
                InvoiceDate = dr.InvoiceDate

            };
            return pd;

        }
    }
}
