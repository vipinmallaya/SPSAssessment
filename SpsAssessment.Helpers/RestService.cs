using SpsAssessment.Helpers.Abstractions;

namespace SpsAssessment.Helpers
{
    public class RestService : IRestService
    {
        public void SendData(string Request)
        {
            Console.WriteLine($"Order Infomration {Request}" );
        }
    }
}
