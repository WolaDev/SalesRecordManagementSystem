using Microsoft.AspNetCore.Mvc;
using SalesRecordManagementSystem.Context;
using SalesRecordManagementSystem.Models;

namespace SalesRecordManagementSystem.Controllers
{
    public class ProductController : Controller
    {

        private readonly SRSContext _context;

        public ProductController(SRSContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateProduct()
        {
            var productModel = new Product()
            {
                Name = "",
                StockAmount = 0,
                Description = "",
                Category = ""
            };

            return View(productModel);
        }

        [HttpPost]
        public IActionResult CreateBusiness(Product model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
