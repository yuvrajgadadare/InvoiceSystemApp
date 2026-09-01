using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystemApp.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
