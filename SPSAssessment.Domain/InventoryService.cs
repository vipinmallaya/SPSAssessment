using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain
{
    public class InventoryService : IInventoryService
    {
        public ArticleInventory GetInventoryForArticle(Article article)
        {
            Random rnd = new Random();
            return new ArticleInventory()
            {
                EANCode = article.EANCode,
                ReservedCount = 10,
                StockCount = 20,
                InventoryID = rnd.NextInt64(),
            };
        }

        public void UpdateInventoryForArticle(Article article)
        {
            Console.WriteLine($"Updated Inventory for article - {article.EANCode} with unit count {article.Quantity}");
        }
    }
}
