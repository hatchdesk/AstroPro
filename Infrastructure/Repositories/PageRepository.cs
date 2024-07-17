using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces.Repositories;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.PageContent;
using Web.Domian.Entities;
using Web.Infrastructure.Repositories.Base;

namespace Web.Infrastructure.Repositories
{
    public class PageRepository :Repository<Page> , IPageRepository
    {
        private readonly WebDbContext _dbContext;
        public PageRepository(WebDbContext context) : base(context)
        {

            _dbContext = context;

        }
        public async Task<Page?> GetByNameAsync(string name)
        {
            return await _dbContext.Pages
                .Include(p => p.Contents)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public  async Task<Page?> GetPageContentAsync()
        {

            return await _dbContext.Pages
          .Include(p => p.Contents) 
          .FirstOrDefaultAsync();

        }
    }
}

