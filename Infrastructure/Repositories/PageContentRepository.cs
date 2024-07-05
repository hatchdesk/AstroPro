using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web.Application.Interfaces.Repositories;
using Web.Application.ViewModels.Admin.PageContent; // Adjust namespaces as needed
using Web.Domian.Entities;
using Web.Infrastructure.Repositories.Base;

namespace Web.Infrastructure.Repositories
{
    public class PageContentRepository : Repository<PageContent>, IPageContentRepository
    {
        private readonly WebDbContext _webDbContext;

        public PageContentRepository(WebDbContext webDbContext) : base(webDbContext)
        {
            _webDbContext = webDbContext;
        }

      
    }
}
