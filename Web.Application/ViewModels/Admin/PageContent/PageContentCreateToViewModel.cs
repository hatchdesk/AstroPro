using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Admin.PageContent
{
    public class PageContentCreateToViewModel
    {
        [Required(ErrorMessage ="Tag is Required")]
        public string Tag { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is Required")]
        public string? Content { get; set; }
        //public string BackgroundColor { get; set; } = string.Empty;
        public int PageId { get; set; }
    }
}
