
namespace Web.Application.ViewModels.Admin.PageContent
{
    public class PageContentToViewModel
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
        public string? Content { get; set; }

        //public string BackgroundColor { get; set; } = string.Empty;
        public int PageId { get; set; }
    }
}
