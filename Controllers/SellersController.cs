using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController(SellerService sellerService, DepartmentService departmentService) : Controller
    {
        private readonly SellerService _sellerService = sellerService;
        private readonly DepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();

            return View(sellers);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var sellerViewModel = new SellerFormViewModel(departments);

            return View(sellerViewModel);
        }

        [HttpPost] // Annotation
        [ValidateAntiForgeryToken] // CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
