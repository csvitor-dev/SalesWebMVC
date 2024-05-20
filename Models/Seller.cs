using System.ComponentModel.DataAnnotations;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name= "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString= "{0:C2}")]
        public double BaseSalary { get; set; }

        public int DepartmentID { get; set;  }
        public Department Department { get; set; } = null!;
        public ICollection<SalesRecord> Sales { get; } = null!;

        public Seller() => Sales = [];
        public Seller(int id, string? name, string? email, DateTime birthDate, double baseSalary, Department department) : this()
        {
            ID = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;

            Department = department;
            DepartmentID = department.ID;
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
