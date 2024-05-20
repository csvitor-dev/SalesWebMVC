using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Data;

namespace SalesWebMVC.Services
{
    public class DepartmentService(SalesWebMvcContext context)
    {
        private readonly SalesWebMvcContext _context = context;

        public async Task<List<Department>> FindAllAsync() => await _context.Department.OrderBy(department => department.Name).ToListAsync();
    }
}
