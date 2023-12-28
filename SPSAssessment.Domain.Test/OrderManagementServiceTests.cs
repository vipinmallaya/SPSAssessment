using NSubstitute;
using NUnit.Framework;
using SpsAssessment.Helpers.Abstractions;
using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;
using System.Reflection.PortableExecutable;

namespace SPSAssessment.Domain.Test
{
     
    public class OrderManagementServiceTests
    {
        IOrderManagementService orderManagementService;
        private IXMLSerializer xmlSerializerSub;
        private IRestService restServiceSerializerSub;

        [SetUp]
        public void Setup()
        {
            xmlSerializerSub = Substitute.For<IXMLSerializer>();
            restServiceSerializerSub = Substitute.For<IRestService>(); 

            orderManagementService = new OrderManagementService(xmlSerializerSub, restServiceSerializerSub);
        }

        [Test]
        public void SendOrderInformation_WhenOrderPassed_RecievedCalls()
        {
            var order = new Models.Order();
            order.Header.Number = 1234;

            xmlSerializerSub.Serialize(order).Returns("XML");

            orderManagementService.SendOrderInformation(order);

            xmlSerializerSub.Received().Serialize(order);

            restServiceSerializerSub.Received().SendData("XML");
        }
    }
}