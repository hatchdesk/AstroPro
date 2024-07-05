using System.Threading.Tasks;
using Web.Application.Interfaces.Repositories.Base;
using Web.Application.ViewModels.Admin.PageContent; // Adjust namespaces as needed
using Web.Domian.Entities;

namespace Web.Application.Interfaces.Repositories
{
    public interface IPageContentRepository : IRepository<PageContent>
    {
       
    }
}
