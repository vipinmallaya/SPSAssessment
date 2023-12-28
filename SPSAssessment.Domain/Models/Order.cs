namespace SPSAssessment.Domain.Models
{
    public class Order
    {
        public OrderHeader Header { get; set; }
        public List<Article> Articles { get; set; }

        public Order()
        {
            Articles = new List<Article>();
            Header = new OrderHeader();
        }
    }
}
