using InvoiceSystemApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceSystemApp.Controllers
{
    public class InvoiceController : Controller
    {
        ICustomerService customerService;
        IProductService productService;
        IInvoiceService invoiceService;
        public InvoiceController(ICustomerService customerService, IProductService productService,IInvoiceService invoiceService)
        {
            this.customerService = customerService;
            this.productService = productService;
            this.invoiceService = invoiceService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> NewInvoice()
        {
            ViewBag.customers = new SelectList(await customerService.GetCustomers(), "CustomerId", "CustomerName");
            ViewBag.products = new SelectList(await productService.GetProducts(),"ProductId", "ProductName");

            return View();
        }

         
    }
}
