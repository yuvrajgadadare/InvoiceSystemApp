using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystemApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
