using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web.Application.ViewModels.Admin.Articles
{
    public class ArticleToCreateViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Title is Required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is Required")]
        public string? Content { get; set; }
        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public string? Category { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }
    }
}
