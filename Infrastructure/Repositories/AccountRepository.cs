using Web.Application.Interfaces.Repositories;
using Web.Domian.Entities;
using Web.Infrastructure.Repositories.Base;

namespace Web.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account> , IAccountRepository
    {
        private readonly WebDbContext _dbContext;
        public AccountRepository(WebDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
