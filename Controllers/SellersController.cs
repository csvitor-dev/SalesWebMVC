using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

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

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var seller = _sellerService.FindByID(id.Value);

            if (seller == null) return NotFound();

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
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var seller = _sellerService.FindByID(id.Value);

            if (seller == null) return NotFound();

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var seller = _sellerService.FindByID(id.Value);

            if (seller == null) return NotFound();
            var departments = _departmentService.FindAll();
            SellerFormViewModel sellerViewModel = new(departments) { Seller = seller };

            return View(sellerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.ID) return BadRequest($"ID conflit: Id: {id}, seller.ID: {seller.ID}");
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
