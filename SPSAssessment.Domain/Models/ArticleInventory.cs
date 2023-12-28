namespace SPSAssessment.Domain.Models
{
    public class ArticleInventory
    {
        public long EANCode { get; set; }
        public long InventoryID { get; set; }
        public long StockCount { get; set; }
        public long ReservedCount { get; set; }

        public long FreeAvailableCount
        {
            get => StockCount - ReservedCount;
        }

    }
}
