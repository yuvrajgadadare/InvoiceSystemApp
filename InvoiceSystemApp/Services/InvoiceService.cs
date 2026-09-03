using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<InvoiceModelDTO> GetInvoice(int Id)
        {
            TblinvoiceDetail d = await db.TblinvoiceDetails.FindAsync(Id);
            InvoiceModelDTO md = GetInvoiceModel(d);
            return md;
        }

        public async Task<List<InvoiceModelDTO>> GetInvoices()
        {
            List<InvoiceModelDTO> lst = new List<InvoiceModelDTO>();
            foreach(TblinvoiceDetail d in await db.TblinvoiceDetails.ToListAsync())
            {
                InvoiceModelDTO md = GetInvoiceModel(d);

                lst.Add(md);

            }
            return lst;
        }

        private InvoiceModelDTO GetInvoiceModel(TblinvoiceDetail d)
        {
            Tblcustomer c = db.Tblcustomers.Find(d.CustomerId);

            List<TblinvoicePayment> payments = db.TblinvoicePayments.Where(e => e.InvoiceId.Equals(d.InvoiceId)).ToList();
            double PaidAmount = 0, RemainingAmount = 0;
            if (payments != null)
            {
                PaidAmount = (float)payments.Sum(e => e.PaymentAmount);
            }
            RemainingAmount =(double)d.InvoiceAmount - PaidAmount;
            string status = "";
            if (PaidAmount == 0)
            {
                status = "Un Paid";
            }
            else if (PaidAmount > 0 && PaidAmount < d.InvoiceAmount)
            {
                status = "Partial Paid";
            }
            else
            {
                status = "Paid";
            }
            InvoiceModelDTO md = new InvoiceModelDTO()
            {
                CustomerId = d.CustomerId,
                CustomerName = c.CustomerName,
                InvoiceAmount = d.InvoiceAmount,
                InvoiceDate = d.InvoiceDate,
                InvoiceId = d.InvoiceId,
                PaidAmount = PaidAmount,
                RemainingAmount = RemainingAmount,
                Status = status
            };
            return md;
        }
    }
}
