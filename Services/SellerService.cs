using SalesWebMVC.Models;
using SalesWebMVC.Data;

namespace SalesWebMVC.Services
{
    public class SellerService(SalesWebMvcContext context)
    {
        private readonly SalesWebMvcContext _context = context;

        public List<Seller> FindAll() => [.. _context.Seller]; // For now, it will be synchronous

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }
    }
}
