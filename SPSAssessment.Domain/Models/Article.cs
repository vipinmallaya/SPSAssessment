namespace SPSAssessment.Domain.Models
{
    public class Article
    {
        public long EANCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }
}
