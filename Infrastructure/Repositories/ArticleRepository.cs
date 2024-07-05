
using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces.Repositories;
using Web.Domian.Entities;
using Web.Infrastructure.Repositories.Base;

namespace Web.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly WebDbContext _dbContext;
        public ArticleRepository(WebDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<List<Article>> GetActiveArticlesAsync()
        {
            return await _dbContext.Articles
                .Where(a => a.IsPublished) 
                .ToListAsync();
        }

    }
}
