using SpsAssessment.Helpers.Attributes;

namespace SPSAssessment.OrderProcessor.Models
{
    public class Article
    {
        [FixedSizeField(0, 13)]
        public string EANCode { get; set; }

        [FixedSizeField(13, 65)]
        public string Description { get; set; }

        [FixedSizeField(78, 10)]
        public string Quantity { get; set; }

        [FixedSizeField(88, 10)]
        public string UnitPrice { get; set; }
    }
}
