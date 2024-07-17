using System.Data;
using Microsoft.EntityFrameworkCore;
using Web.Domian.Entities;

namespace Web.Infrastructure
{
    public class WebDbContext :DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> Options) : base(Options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PageContent> PageContents { get; set; }

    }
}
