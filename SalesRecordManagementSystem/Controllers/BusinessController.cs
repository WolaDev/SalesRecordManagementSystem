using Microsoft.AspNetCore.Mvc;
using SalesRecordManagementSystem.Context;
using SalesRecordManagementSystem.Dto;
using SalesRecordManagementSystem.Models;

namespace SalesRecordManagementSystem.Controllers
{
    public class BusinessController : Controller
    {
        private readonly SRSContext _context;

        public BusinessController(SRSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var businesses = _context.Businesses.ToList();
            return View(businesses);
        }

        /*public IActionResult GetAllBusinesses()
        {
            var businesses = _context.Businesses.ToList();

            return View(businesses);
        }*/
        public IActionResult CreateBusiness()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateBusiness(CreateBusinessDto model)
        {
            if (ModelState.IsValid)
            {
                var businessModel = new Business()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Type = model.Type,
                    Description = model.Description,
                    Email = model.Email
                };

                _context.Businesses.Add(businessModel);

                if(_context.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
          return RedirectToAction("CreateBusiness");
        }

        public IActionResult EditBusiness(int businessId) 
        {
            var business = _context.Businesses.Find(businessId);

            var businessModel = new Business()
            {
                Name = business.Name,
                Type = business.Type,
                Description = business.Description,
                Email = business.Email
            };

            return View(businessModel);
        }

        [HttpPost]
        public IActionResult EditBusiness(int businessId, Business model)
        {
            if(ModelState.IsValid) 
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult DeleteBusiness(int businessId) 
        {
            var business = _context.Businesses.Find(businessId);

            var businessModel = new Business()
            {
                Name = business.Name,
                Type = business.Type,
                Email = business.Email
            };

            return View(businessModel);
        }

        [HttpPost]
        public IActionResult DeleteBusiness(int businessId, Business model)
        {
            if(ModelState.IsValid)
            {
                var business = _context.Businesses.Find(businessId);
                _context.Businesses.Remove(business);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
