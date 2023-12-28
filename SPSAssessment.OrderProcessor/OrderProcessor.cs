using SpsAssessment.Helpers.Abstractions;
using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;
using System.Globalization;
using System.Text;
using Article = SPSAssessment.OrderProcessor.Models.Article;
using DomainArticle = SPSAssessment.Domain.Models.Article;
using DomainOrder = SPSAssessment.Domain.Models.Order;
using OrderHeader = SPSAssessment.OrderProcessor.Models.OrderHeader;

namespace SPSAssessment.OrderProcessor
{
    public class OrderProcessor : IFixedLengthFileProcessor
    {
        IOrderService _orderReciever;
        private IFixedLengthContentDeserializer _fixedLengthContentDeserializer;
        private OrderHeader _orderHeader;
        private List<Article> _articles;
        private DomainOrder _order;

        public OrderProcessor(IOrderService orderReciever, IFixedLengthContentDeserializer fixedLengthContentDeserializer)
        {
            _orderReciever = orderReciever;
            _fixedLengthContentDeserializer = fixedLengthContentDeserializer;
        }

        public async Task<string> ProcessFileAsyc(string filePath)
        {
            if (!ValidateFilePath(filePath, out var message))
            {
                return message;
            }

            const int BufferSize = 128;
            using (var fileStream = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                message = await ProcessFileContent(streamReader);
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                message = GenerateOrder(_orderHeader, _articles);
            }

            _orderReciever.RecieveOrder(_order);

            return message;
        }

        private async Task<string> ProcessFileContent(StreamReader streamReader)
        {
            string line;
            var lineCounter = 0;

            _orderHeader = new OrderHeader();
            _articles = new List<Article>();
            var message = string.Empty;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (lineCounter == 0)
                {
                    if (line.Length <= 75)
                    {
                        message = "Invalid order header encountered; Aborting operation";
                        break;
                    }

                    _orderHeader = DeserializeAndAssign(line, _orderHeader);
                }
                else
                {
                    if (line.Length <= 97)
                    {
                        message = "Invalid article encountered; Aborting operation";
                        break;
                    }

                    var article = new Article();
                    _articles.Add(DeserializeAndAssign(line, article));
                }

                lineCounter++;
            }  

            return message; 
        }

        private T DeserializeAndAssign<T>(string line, T targetObject)
        {
            return _fixedLengthContentDeserializer.Deserialize(line, targetObject);
        }

        private string GenerateOrder(OrderHeader orderHeader, List<Article> articles)
        {
            _order = new DomainOrder();

            if (!Enum.TryParse<FileType>(orderHeader.FileTypeIdentifer, out var fileType))
            {
                return "InValid FileType Identifier! process aborting";
            }
            if (!long.TryParse(orderHeader.Number, out var orderNumber))
            {
                return "InValid OrderNumber Identifier! process aborting";
            }

            if (!DateTime.TryParseExact(orderHeader.Date,
                      "yyyyMMddTHHmm",
                      CultureInfo.InvariantCulture,
                      DateTimeStyles.None,
                      out var orderDate))
            {
                return "InValid Date";
            }
            if (!long.TryParse(orderHeader.SupplierEANCode, out var supplierEAN))
            {
                return "InValid SupplierEANCode! process aborting";
            }
            if (!long.TryParse(orderHeader.BuyerEANCode, out var buyerEAN))
            {
                return "InValid SupplierEANCode! process aborting";
            } 

            _order.Header.Number = orderNumber;
            _order.Header.Comments = orderHeader.Comments;
            _order.Header.BuyerEANCode = buyerEAN;
            _order.Header.SupplierEANCode = supplierEAN;
            _order.Header.Date = orderDate;

            foreach (var item in _articles)
            {
                var articleMessage = GenerateDomainArticle(item, out var domainArticle);
                if (!string.IsNullOrEmpty(articleMessage))
                {
                    _order.Articles.Clear();

                    return articleMessage;
                }

                _order.Articles.Add(domainArticle);
            }
            return "Completed Processing";
        }

        public string GenerateDomainArticle(Article article, out DomainArticle domainArticle)
        {
            domainArticle = new DomainArticle();
            if (!long.TryParse(article.EANCode, out var articleEanCode))
            {
                return "InValid article EAN Code! process aborting.";
            }

            if (!float.TryParse(article.UnitPrice, out var unitPrice))
            {
                return "InValid article unit price! process aborting.";
            }
            if (!int.TryParse(article.Quantity, out var quantity))
            {
                return "InValid article EAN Code! process aborting.";
            }

            domainArticle.UnitPrice = unitPrice;
            domainArticle.Quantity = quantity;
            domainArticle.Description = article.Description;
            domainArticle.EANCode = articleEanCode;

            return string.Empty;
        }

        private bool ValidateFilePath(string filePath, out string message)
        {
            message = string.Empty;
            var validFile = true;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                message = $"No file path provided";
                validFile = false;
            }
            else if (!File.Exists(filePath))
            {
                message = $"File doesnot exists in path-{filePath}";
                validFile = false;
            }

            return validFile;
        }
    }
}