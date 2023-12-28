using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Abstractions
{
    public interface IOrderManagementService
    {
        void SendOrderInformation(Order order);
    }
}
