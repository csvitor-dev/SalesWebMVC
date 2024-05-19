using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController(SellerService sellerService) : Controller
    {
        private readonly SellerService _sellerService = sellerService;

        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();

            return View(sellers);
        }
    }
}
