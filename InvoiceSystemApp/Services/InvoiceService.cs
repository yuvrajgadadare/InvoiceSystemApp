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
            //InvoiceDTO pd = new InvoiceDTO()
            //{
            //    InvoiceId = dr.InvoiceId,
            //    CustomerId = dr.CustomerId,
            //    InvoiceAmount = dr.InvoiceAmount,
            //    InvoiceDate = dr.InvoiceDate

            //};
            d.InvoiceId = dr.InvoiceId;
            return d;

        }

        public async Task<InvoiceModelDTO> GetInvoice(int Id)
        {
            TblinvoiceDetail d = await db.TblinvoiceDetails.FindAsync(Id);
            InvoiceModelDTO md = await GetInvoiceModel(d);
            return md;
        }

        public async Task<List<InvoiceModelDTO>> GetInvoices()
        {
            List<InvoiceModelDTO> lst = new List<InvoiceModelDTO>();
            foreach(TblinvoiceDetail d in await db.TblinvoiceDetails.ToListAsync())
            {
                InvoiceModelDTO md = await GetInvoiceModel(d);

                lst.Add(md);

            }
            return lst;
        }

        public async Task<PaymentFormDTO> SubmitPayment(PaymentFormDTO p)
        {
            TblinvoicePayment pay = new TblinvoicePayment()
            {
                InvoiceId = p.InvoiceId,
                PaymentDate = p.PaymentDate,
                PaymentAmount = p.PaymentAmount,
                PaymentDescription = p.PaymentDescription,
                PaymentMode = p.PaymentMode
            };
            await db.TblinvoicePayments.AddAsync(pay);
            await db.SaveChangesAsync();
            p.PaymentId = pay.PaymentId;
            return p;
        }

        private async Task<InvoiceModelDTO> GetInvoiceModel(TblinvoiceDetail d)
        {
            Tblcustomer c = await db.Tblcustomers.FindAsync(d.CustomerId);

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
                Status = status,
                //  Products= GetInvoiceWiseProducts(d.InvoiceId),
                 // Payments=  GetInvoiceWisePayments(d.InvoiceId) 
                    
            };

            return md;
        }


        public async Task< List<InvoiceProductDTO>> GetInvoiceWiseProducts(int Id)
        {
            List<InvoiceProductDTO> lst = new List<Dtos.InvoiceProductDTO>();
            foreach(TblinvoiceProduct p in await db.TblinvoiceProducts.Where(e=>e.InvoiceId.Equals(Id)).ToListAsync())
            {
                Tblproduct pr = db.Tblproducts.Find(p.ProductId);
                double total=(pr.Rate+(pr.Rate*pr.Gst/100))*p.Quantity;
                InvoiceProductDTO pd = new InvoiceProductDTO()
                {
                    ProductId = pr.ProductId,
                    ProductName = pr.ProductName,
                    Rate = pr.Rate,
                    Gst = pr.Gst,
                    Quantity = p.Quantity,
                    TotalAmount = total
                };
                lst.Add(pd);

            }
            return lst;
        }


        public async Task<List<PaymentFormDTO>> GetInvoiceWisePayments(int Id)
        {
            List<PaymentFormDTO> lst = new List<PaymentFormDTO>();
            foreach(TblinvoicePayment p in await db.TblinvoicePayments.Where(e=>e.InvoiceId.Equals(Id)).ToListAsync())
            {
                lst.Add(new PaymentFormDTO { 
                 InvoiceId=p.InvoiceId,
                  PaymentAmount=p.PaymentAmount,
                   PaymentDate=p.PaymentDate,
                    PaymentDescription=p.PaymentDescription,
                     PaymentId=p.PaymentId,
                      PaymentMode=p.PaymentMode
                });
            }
            return lst;
        }

        private async Task<CustomerDTO> GetCustomer(int Id)
        {
            Tblcustomer c = await db.Tblcustomers.FindAsync(Id);
            CustomerDTO cr = new CustomerDTO()
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                City = c.City,
                EmailAddress = c.EmailAddress,
                MobileNumber = c.MobileNumber
            };
            return cr;
        }

        

        
    }
}
