using System.Threading.Tasks;
using Web.Application.ViewModels.Admin.PageContent;

namespace Web.Application.Interfaces.Services
{
    public interface IPageContentService
    {
        Task<bool> DeletePageContentAsync(int id);
        Task<PageContentToViewModel?> GetPageContentAsync(int id);
        Task<PageContentToViewModel?> UpdatePageContentAsync(PageContentEditToViewModel model);
       
    }
}
