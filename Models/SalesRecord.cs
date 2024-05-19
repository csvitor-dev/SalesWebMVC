using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int ID { get; }
        public DateTime Date { get; set; }
        public double Amout { get; set; }
        public ESaleStatus Status { get; set; }
        public int SellerID { get; set; }
        public Seller Seller { get; set; } = null!;

        public SalesRecord() { }
        public SalesRecord(int id, DateTime date, double amout, ESaleStatus status, Seller seller)
        {
            ID = id;
            Date = date;
            Amout = amout;
            Status = status;
            Seller = seller;
            SellerID = seller.ID;
        }
    }
}
