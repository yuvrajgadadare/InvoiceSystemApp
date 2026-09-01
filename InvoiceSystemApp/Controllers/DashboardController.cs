using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystemApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
