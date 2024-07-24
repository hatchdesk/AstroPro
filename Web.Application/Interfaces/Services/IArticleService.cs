
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Page;

namespace Web.Application.Interfaces.Services
{
    public interface IArticleService
    {
       Task<List<ArticleToCreateViewModel>> GetAllArticlesAsync();
        Task<List<ArticleToViewModel>> GetActiveArticlesAsync();
        Task<ArticleToViewModel?> AddArticleAsync(ArticleToCreateViewModel model);
        Task<ArticleToViewModel?> GetArticle(int Id);

        Task<ArticleToCreateViewModel> UpdateImage(int id, string userProfileImage);
        Task<ArticleToCreateViewModel?> UpdateArticleAsync(ArticleToEditViewModel model);
        Task<bool> DeleteArticleAsync(int  Id);
        Task<PageParentModel?> GetHomePage();
    }
}
