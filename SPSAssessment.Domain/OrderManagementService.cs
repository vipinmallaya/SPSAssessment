using SpsAssessment.Helpers.Abstractions;
using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain
{
    public class OrderManagementService : IOrderManagementService
    {
        private IXMLSerializer _xmlSerializer;
        private IRestService _restService;

        public OrderManagementService(IXMLSerializer xmlSerializer, IRestService restService)
        {
            _xmlSerializer = xmlSerializer;
            _restService = restService;
        }

        public void SendOrderInformation(Order order)
        {
            var xmlData = _xmlSerializer.Serialize(order);
            //TODO::Send XML Data
            _restService.SendData(xmlData);
        }
    }
}
