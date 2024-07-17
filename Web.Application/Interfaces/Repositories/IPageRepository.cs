using Web.Application.Interfaces.Repositories.Base;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.PageContent;
using Web.Domian.Entities;

namespace Web.Application.Interfaces.Repositories
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<Page?> GetByNameAsync(string name);
        Task<Page?> GetPageContentAsync();
    }
}
