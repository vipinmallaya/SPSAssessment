using SpsAssessment.Helpers.Attributes;

namespace SPSAssessment.OrderProcessor.Models
{
    public class OrderHeader
    {
        [FixedSizeField(0, 3)]
        public string FileTypeIdentifer { get; set; }

        [FixedSizeField(3, 20)]
        public string Number { get; set; }

        [FixedSizeField(23, 13)]
        public string Date { get; set; }
        [FixedSizeField(36, 13)]
        public string BuyerEANCode { get; set; }
        [FixedSizeField(49, 13)]
        public string SupplierEANCode { get; set; }
        [FixedSizeField(62, 100)]
        public string Comments { get; set; }


    }
}
