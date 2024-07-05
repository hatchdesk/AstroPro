using Web.Application.ViewModels.Admin.PageContent;

namespace Web.Application.ViewModels.Admin.Page
{
    public class PageToViewModel
    { 
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public List<PageContentToViewModel>? Contents { get; set; }
    }
}
