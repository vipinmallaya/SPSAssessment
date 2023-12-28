using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain
{
    public class NotificationService : INotificationService
    {
        public void NotifyInventoryNotAvailable(Article article)
        {

        }

        public void NotifyMisMatchInUnitPrice(Article article, float supplierPrice, OrderHeader orderHeader)
        {
            Console.WriteLine($"Miss match in price for Order - {orderHeader.Number}. ");
            Console.WriteLine($"\nNotification sending to manager. ");
            Console.WriteLine($"\nArticle EAN {article.EANCode} Article Price {article.UnitPrice}  Supplier Price {supplierPrice}\n");
        }
    }
}
