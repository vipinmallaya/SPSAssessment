using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Abstractions
{
    public interface INotificationService
    {
        void NotifyMisMatchInUnitPrice(Article article, float supplierPrice, OrderHeader orderHeader);
        void NotifyInventoryNotAvailable(Article article);
    }
}
