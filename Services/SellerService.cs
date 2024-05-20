using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;
using SalesWebMVC.Data;
using SalesWebMVC.Services.Exceptions;

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

        public Seller? FindByID(int? id) => _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.ID == id);

        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);

            if (seller == null) throw new NotFoundException(nameof(seller));
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (_context.Seller.Any(dbSeller => dbSeller.ID == seller.ID) == false)
                throw new NotFoundException("ID not found");
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
