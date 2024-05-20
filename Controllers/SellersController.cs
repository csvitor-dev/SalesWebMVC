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

        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.FindAllAsync();

            return View(sellers);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var sellerViewModel = new SellerFormViewModel(departments);

            return View(sellerViewModel);
        }
        [HttpPost] // Annotation
        [ValidateAntiForgeryToken] // CSRF
        public async Task<IActionResult> Create(Seller seller)
        {
            if (ModelState.IsValid == false)
            {
                var departments = await _departmentService.FindAllAsync();

                return View(new SellerFormViewModel(departments)
                {
                    Seller = seller
                });
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
            var seller = await _sellerService.FindByIDAsync(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });

            return View(seller);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { ex.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
            var seller = await _sellerService.FindByIDAsync(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "ID not provided" });
            var seller = await _sellerService.FindByIDAsync(id.Value);

            if (seller == null) return RedirectToAction(nameof(Error), new { Message = "ID not found" });
            var departments = await _departmentService.FindAllAsync();
            SellerFormViewModel sellerViewModel = new(departments) { Seller = seller };

            return View(sellerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (ModelState.IsValid == false)
            {
                var departments = await _departmentService.FindAllAsync();

                return View(new SellerFormViewModel(departments)
                {
                    Seller = seller
                });
            }

            if (id != seller.ID) return RedirectToAction(nameof(Error), new { Message = "ID mismatch" });
            try
            {
                await _sellerService.UpdateAsync(seller);
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
