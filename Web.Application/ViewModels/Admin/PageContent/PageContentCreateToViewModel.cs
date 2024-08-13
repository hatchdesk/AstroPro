using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Web.Application.ViewModels.Admin.PageContent
{
    public class PageContentCreateToViewModel
    {
        [Required(ErrorMessage ="Tag is Required")]
        public string Tag { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is Required")]
        public string? Content { get; set; }

		//public string? brandlogo_Url { get; set; }
		//public IFormFile? brandlogo { get; set; }
		//public string Background_Color { get; set; } = string.Empty;
		public int PageId { get; set; }
    }
}
