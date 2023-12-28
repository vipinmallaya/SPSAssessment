using SPSAssessment.Domain.Models;

namespace SPSAssessment.Domain.Abstractions
{
    public interface IArticleService
    {
        void ProcessArticle(OrderHeader orderHeader, Article orderArticleItem);
    }
}
