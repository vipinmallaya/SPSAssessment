using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Abstractions
{
    public interface IOrderService
    {
        void RecieveOrder(Order order);

    }
}
