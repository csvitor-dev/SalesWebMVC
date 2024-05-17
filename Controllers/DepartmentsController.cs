using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private List<Department> _departments = new();
        public IActionResult Index()
        {
            _departments.Add(new(1, "Eletronics"));
            _departments.Add(new(2, "Fashion"));

            return View(_departments);
        }
    }
}
