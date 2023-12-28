using NSubstitute;
using NUnit.Framework;
using SPSAssessment.Domain.Abstractions;
using SpsAssessment.Helpers.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Test
{
    public class OrderServiceTests
    {
        private OrderService orderService;
        private IArticleService articleServiceSub;
        private IOrderManagementService orderManagementSub;

        [SetUp]
        public void Setup()
        {
            articleServiceSub = Substitute.For<IArticleService>();
            orderManagementSub = Substitute.For<IOrderManagementService>();

            orderService = new OrderService(articleServiceSub, orderManagementSub);
        }

        [Test]
        public void RecieveOrder_WhenCalledWithValidOrders_InvokedSendOrderInformation()
        {
            var order = new Order();
            order.Header.Number = 1;
            order.Header.SupplierEANCode = 222;
            order.Articles = new List<Article>()
            {
                new Article()
                {

                },
                new Article()
                {

                }

            };
            orderService.RecieveOrder(order);

            articleServiceSub.Received(2).ProcessArticle(order.Header, Arg.Any<Article>(), 222);
            orderManagementSub.Received(1).SendOrderInformation(order);
        } 
    }
}
