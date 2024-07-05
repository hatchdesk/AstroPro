
using Web.Application.ViewModels.Admin.Articles;

namespace Web.Application.Interfaces.Services
{
    public interface IArticleService
    {
       Task<List<ArticleToViewModel>> GetAllArticlesAsync();
        Task<List<ArticleToViewModel>> GetActiveArticlesAsync();
        Task<ArticleToViewModel?> AddArticleAsync(ArticleToCreateViewModel model);
        Task<ArticleToViewModel?> GetArticle(int Id);

        Task<ArticleToViewModel?> UpdateArticleAsync(ArticleToEditViewModel model);
        Task<bool> DeleteArticleAsync(int  Id);
    }
}
