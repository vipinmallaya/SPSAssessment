using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain
{
    public class ArticleService : IArticleService
    {
        private INotificationService _notificationService;
        private IInventoryService _inventoryService;
        private IArticlePriceService _articlePriceService;
        public ArticleService(INotificationService notificationService, IInventoryService inventoryService, IArticlePriceService articlePriceService)
        {
            _notificationService = notificationService;
            _inventoryService = inventoryService;
            _articlePriceService = articlePriceService;
        }

        public void ProcessArticle(OrderHeader orderHeader, Article orderArticleItem)
        {
            var supplierArticlePrice = _articlePriceService.GetArticlePriceFromSupplier(orderHeader.SupplierEANCode, orderArticleItem);
            if (supplierArticlePrice.UnitPrice != orderArticleItem.UnitPrice)
            {
                _notificationService.NotifyMisMatchInUnitPrice(orderArticleItem, supplierArticlePrice.UnitPrice, orderHeader);
            }

            orderArticleItem.UnitPrice = supplierArticlePrice.UnitPrice;

            var articleInventory = _inventoryService.GetInventoryForArticle(orderArticleItem);
            if (articleInventory.FreeAvailableCount < orderArticleItem.Quantity)
            {
                _notificationService.NotifyInventoryNotAvailable(orderArticleItem);
            }

            _inventoryService.UpdateInventoryForArticle(orderArticleItem);
        }
    }
}
