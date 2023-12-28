namespace SPSAssessment.Domain.Models
{
    public class ArticlePrice
    {
        public long EANCode { get; set; }
        public long ArticlePriceId { get; set; }
        public long SupplierEANCode { get; set; }
        public float UnitPrice { get; set; }
    }
}
