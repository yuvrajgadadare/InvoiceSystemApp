using InvoiceSystemApp.Dtos;
using InvoiceSystemApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystemApp.Services
{
    public class CustomerService : ICustomerService
    {
        InvoiceSystemDbContext db;
        public CustomerService(InvoiceSystemDbContext db)
        {
            this.db = db;
        }
        public async Task<CustomerDTO> GetCustomer(int Id)
        {
            Tblcustomer c =await db.Tblcustomers.FindAsync(Id);
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

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            List<CustomerDTO> lst = new List<CustomerDTO>();
            foreach(Tblcustomer c in await db.Tblcustomers.ToListAsync())
            {
                CustomerDTO cr = new CustomerDTO()
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    City = c.City,
                    EmailAddress = c.EmailAddress,
                    MobileNumber = c.MobileNumber
                };
                lst.Add(cr);
            }
            return lst;
        }
    }
}
