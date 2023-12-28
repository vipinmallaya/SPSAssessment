using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Abstractions
{
    public interface IInventoryService
    {
        ArticleInventory GetInventoryForArticle(Article article);
        void UpdateInventoryForArticle(Article article);
    }
}
