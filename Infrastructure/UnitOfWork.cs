using Web.Application.Interfaces;

namespace Web.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebDbContext _context;

        public UnitOfWork(WebDbContext context)
        {
            this._context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
