using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

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
            if (ModelState.IsValid == false) return View(seller);

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
            var seller = _sellerService.FindByID(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });

            return View(seller);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                _sellerService.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { ex.Message });
            }
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
            var seller = _sellerService.FindByID(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
            var seller = _sellerService.FindByID(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });
            var departments = _departmentService.FindAll();
            SellerFormViewModel sellerViewModel = new(departments) { Seller = seller };

            return View(sellerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (ModelState.IsValid == false)
            {
                var departments = _departmentService.FindAll();
                
                return View(new SellerFormViewModel(departments)
                {
                    Seller = seller
                });
            }
            
            if (id != seller.ID) return RedirectToAction(nameof(Error), new { Message = "ID mismatch" });
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { ex.Message });
            }
        }

        public IActionResult Error(string? message)
        {
            ErrorViewModel errorViewModel = new()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message
            };

            return View(errorViewModel);
        }
    }
}
