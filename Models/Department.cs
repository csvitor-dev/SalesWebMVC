namespace SalesWebMVC.Models
{
    public class Department
    {
        public int ID { get; }
        public string? Name { get; set; }
        public ICollection<Seller> Sellers { get; } = null!;

        public Department() => Sellers = new List<Seller>();
        public Department(int id, string name) : this()
        {
            ID = id;
            Name = name;
        }

        public void AddSeller(Seller seller) => Sellers.Add(seller);
        
        public double TotalSales(DateTime initial, DateTime final)
        {
            // double query = (from seller in Sellers select seller.TotalSales(initial, final)).Sum();
            double query = Sellers.Sum(seller => seller.TotalSales(initial, final));
            return query;
        }
    }
}
