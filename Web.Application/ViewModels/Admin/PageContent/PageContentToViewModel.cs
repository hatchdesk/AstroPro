
using Microsoft.AspNetCore.Http;

namespace Web.Application.ViewModels.Admin.PageContent
{
    public class PageContentToViewModel
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public string? Content { get; set; }
		//public string? brandlogo_Url { get; set; }
		//public IFormFile? brandlogo { get; set; }
		//public string Background_Color { get; set; } = string.Empty;
        public int PageId { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }

    }
}
