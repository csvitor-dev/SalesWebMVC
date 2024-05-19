using SalesWebMVC.Models;
using SalesWebMVC.Data;

namespace SalesWebMVC.Services
{
    public class DepartmentService(SalesWebMvcContext context)
    {
        private readonly SalesWebMvcContext _context = context;

        public List<Department> FindAll() => [.. _context.Department.OrderBy(department => department.Name)]; // For now, it will be synchronous
    }
}
