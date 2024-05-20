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

        public Seller? FindByID(int? id) => _context.Seller.FirstOrDefault(seller => seller.ID == id);

        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);

            if (seller == null) throw new NullReferenceException(nameof(seller));
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }
    }
}
