using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Domian.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Web.Domian.Enums;

namespace Web.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleToViewModel?> AddArticleAsync(ArticleToCreateViewModel model)
        {

            if (!Enum.TryParse<ArticleCategory>(model.Category, out var articleCategory))
            {
                return null;
            }
            var objarticle = new Article()
            {
                Title = model.Title,
                Content = model.Content,
                IsPublished = model.IsPublished,
                Image = model.ImageUrl,
                Category = articleCategory,
            };

            var addedArticle = await _articleRepository.AddAsync(objarticle);
            var savedArticle = await _unitOfWork.SaveChangesAsync();
            if (savedArticle > 0)
            {
                return new ArticleToViewModel
                {
                    Id = addedArticle.Id,
                };
            }
            return null;
        }

        public async Task<bool> DeleteArticleAsync(int Id)
        {
            var article = await _articleRepository.DeleteAsync(Id);
            var deleterecordes = await _unitOfWork.SaveChangesAsync();
            if (deleterecordes > 0)
                return true;
            return false;
        }

        public async Task<List<ArticleToViewModel>> GetActiveArticlesAsync()
        {
            var activeArticles = await _articleRepository.GetActiveArticlesAsync();
            return activeArticles.Select(e => new ArticleToViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Content = e.Content,
                Category = e.Category.ToString(),
                IsPublished = e.IsPublished
            }).ToList();
        }

        public async Task<List<ArticleToCreateViewModel>> GetAllArticlesAsync()
        {
            var objarticle = await _articleRepository.GetAllAsync();
            return objarticle.Select(e => new ArticleToCreateViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Content = e.Content,
                Category = e.Category.ToString(),
                IsPublished = e.IsPublished,
                ImageUrl = e.Image 
            }).ToList();
        }

        public async Task<ArticleToViewModel?> GetArticle(int Id)
        {
            var data = await _articleRepository.GetAsync(Id);
            if (data == null)
                return null;
            return new ArticleToViewModel()
            {
                Id = data.Id,
                Title = data.Title,
                Content = data.Content,
                Category = data.Category.ToString(),
                IsPublished = data.IsPublished,
               ImageUrl = data.Image
            };
        }

        public async Task<ArticleToViewModel?> UpdateArticleAsync(ArticleToEditViewModel model)
        {
            var updatedArticle = await _articleRepository.GetAsync(model.Id);
            if (updatedArticle == null)
            {
                return new ArticleToViewModel();
            }
            updatedArticle.Title = model.Title;
            updatedArticle.Content = model.Content;
            updatedArticle.IsPublished = model.IsPublished;
            // updatedArticle.Category = model.Category; // Uncomment if Category is being used

            _articleRepository.UpdateAsync(updatedArticle);
            await _unitOfWork.SaveChangesAsync();

            return new ArticleToViewModel()
            {
                Title = updatedArticle.Title,
                Content = updatedArticle.Content,
                IsPublished = updatedArticle.IsPublished,
                Category = updatedArticle.Category.ToString(),
            };
        }

        public async Task<ArticleToCreateViewModel> UpdateImage(int id, string userProfileImage)
        {
            var articleImage = await _articleRepository.GetAsync(id);
            if (articleImage != null)
            {
                articleImage.Image = userProfileImage;
                _articleRepository.UpdateAsync(articleImage);
                await _unitOfWork.SaveChangesAsync();

                return new ArticleToCreateViewModel()
                {
                    Title = articleImage.Title,
                    Content = articleImage.Content,
                    IsPublished = articleImage.IsPublished,
                    Category = articleImage.Category.ToString(),
                    ImageUrl = articleImage.Image,
                };
            }
            return new ArticleToCreateViewModel();
        }
    }
}
