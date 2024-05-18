namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int ID { get; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public int DeparmentID { get; }
        public Department Department { get; set; } = null!;
        public ICollection<SalesRecord> Sales { get; } = null!;

        public Seller() => Sales = new List<SalesRecord>();
        public Seller(int id, string? name, string? email, DateTime birthDate, double baseSalary, Department department) : this()
        {
            ID = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;

            Department = department;
            DeparmentID = department.ID;
        }

        public void AddSales(SalesRecord record) => Sales.Add(record);
        public void RemoveSales(SalesRecord record) => Sales.Remove(record);

        public double TotalSales(DateTime initial, DateTime final)
        {
            // double query = (from record in Sales where record.Date >= initial && record.Date <= final select record.Amout).Sum();
            double query = Sales.Where(record => record.Date >= initial && record.Date <= final).Sum(record => record.Amout);
            return query;
        }
    }
}
