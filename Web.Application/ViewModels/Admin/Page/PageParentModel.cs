using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Service;
using Web.Application.ViewModels.Consultation;
using Web.Application.ViewModels.NewFolder;

namespace Web.Application.ViewModels.Admin.Page
{
    public class PageParentModel
    {
        public PageToViewModel? PageModel { get; set; }
        public List<ServiceToViewModel> ServiceToModel { get; set; } = new List<ServiceToViewModel>();
        public List<ArticleToViewModel> ArticleToModel { get; set; } = new List<ArticleToViewModel>();
        public ConsultationSendToViewModel ?ConsultationToModel { get; set; }  
    }
}
