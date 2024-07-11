using Microsoft.AspNetCore.Http;

namespace Web.Application.ViewModels.Admin.Articles
{
    public class ArticleToViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public bool IsPublished { get; set; }
        public string? Category { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
