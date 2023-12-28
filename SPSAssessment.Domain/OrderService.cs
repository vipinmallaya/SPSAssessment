using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain
{
    public class OrderService : IOrderService
    {
        private IArticleService _articleService;
        private IOrderManagementService _orderManagementService;

        public OrderService(IArticleService articleService, IOrderManagementService orderManagementService)
        {
            _articleService = articleService;
            _orderManagementService = orderManagementService;
        }

        public void RecieveOrder(Order order)
        {
            //TODO::Consider Parallel based on the requests and resources
            foreach (var item in order.Articles)
            {
                _articleService.ProcessArticle(order.Header, item);
            }

            SendOrderToBackend(order);
        }

        private void SendOrderToBackend(Order order)
        {
            _orderManagementService.SendOrderInformation(order);
        }
    }
}
