namespace SPSAssessment.Domain.Models
{
    public class OrderHeader
    { 
        public long Number { get; set; }
        public DateTime Date { get; set; }
        public long BuyerEANCode { get; set; }
        public long SupplierEANCode { get; set; }
        public string Comments { get; set; }
    }
}
