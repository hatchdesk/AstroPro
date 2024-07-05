using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Admin.Articles
{
    public class ArticleToCreateViewModel
    {

        [Required(ErrorMessage ="Title is Required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is Required")]
        public string? Content { get; set; }
        public bool IsPublished { get; set; }
        public string? Category { get; set; }
    }
}
