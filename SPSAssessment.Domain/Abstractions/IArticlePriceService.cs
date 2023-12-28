using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Abstractions
{
    public interface IArticlePriceService
    {
        ArticlePrice GetArticlePriceFromSupplier(long SupplierEANCode, Article article);
    }
}
