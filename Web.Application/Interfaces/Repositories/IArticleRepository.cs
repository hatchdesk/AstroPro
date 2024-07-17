using Web.Application.Interfaces.Repositories.Base;
using Web.Domian.Entities;

namespace Web.Application.Interfaces.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<List<Article>> GetActiveArticlesAsync();
        Task<List<Article>> GetArticlesAsync();
    }
}
