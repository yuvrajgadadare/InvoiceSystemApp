using InvoiceSystemApp.Dtos;
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
        public async Task<IActionResult> Index()
        {
            List<InvoiceModelDTO> lst =await  invoiceService.GetInvoices();
            return View(lst);
        }
        public async Task<IActionResult> NewInvoice()
        {
            ViewBag.customers = new SelectList(await customerService.GetCustomers(), "CustomerId", "CustomerName");
            ViewBag.products = new SelectList(await productService.GetProducts(),"ProductId", "ProductName");

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GenerateInvoice([FromBody] InvoiceDTO d)
        {
            InvoiceDTO sd= await invoiceService.AddInvoice(d);
            return Json(sd);
        }


        public async Task<IActionResult> PayInvoice(int id)
        {
            InvoiceModelDTO m =await invoiceService.GetInvoice(id);
            ViewData["invoice"] = m;
            PaymentFormDTO p = new PaymentFormDTO()
            {
                InvoiceId = id
            };
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> PayInvoice(PaymentFormDTO pd)
        {
          PaymentFormDTO mp=  await invoiceService.SubmitPayment(pd);
            InvoiceModelDTO m = await invoiceService.GetInvoice(pd.InvoiceId);
            ModelState.Clear();
            ViewBag.msg = "Payment accepted successfully";
            ViewData["invoice"] = m;
            PaymentFormDTO p = new PaymentFormDTO()
            {
                InvoiceId = pd.InvoiceId
            };
            return View(p);
        }

        public async Task<IActionResult> ViewInvoice(int id)
        {
            InvoiceModelDTO m =await  invoiceService.GetInvoice(id);
            CustomerDTO c = await customerService.GetCustomer(m.CustomerId);
            ViewData["customer"] = c;
            return View(m); 
        }
    }
}
