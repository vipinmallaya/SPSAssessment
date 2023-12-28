using SPSAssessment.Domain.Abstractions;
using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain
{
    public class ArticlePriceService : IArticlePriceService
    {
        List<ArticlePrice> _articles;

        public ArticlePriceService()
        {
            var rnd = new Random();
            _articles = new List<ArticlePrice>()
            {
                new ArticlePrice()
                {
                    ArticlePriceId = rnd.NextInt64(),
                    EANCode = 8712345678920,
                    SupplierEANCode = 8712345678944,
                    UnitPrice = 13.00f
                }
            };

        }
        public ArticlePrice GetArticlePriceFromSupplier(long SupplierEANCode, Article article)
        {
            var rnd = new Random();
            var articleListItem = _articles.FirstOrDefault(item => item.EANCode == article.EANCode);

            return articleListItem ?? new ArticlePrice()
            { 
                ArticlePriceId = rnd.NextInt64(),
                EANCode = article.EANCode,
                SupplierEANCode = SupplierEANCode,
                UnitPrice = article.UnitPrice
            };
        }
    }
}
