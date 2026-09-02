using InvoiceSystemApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystemApp.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetProduct(int Id)
        {
            return Json(await productService.GetProduct(Id));
        }
    }
}
