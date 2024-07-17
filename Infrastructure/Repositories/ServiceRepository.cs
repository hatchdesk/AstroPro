using Microsoft.EntityFrameworkCore;
using Web.Application.Interfaces.Repositories;
using Web.Domian.Entities;
using Web.Infrastructure.Repositories.Base;

namespace Web.Infrastructure.Repositories
{
    public class ServiceRepository: Repository<Service> , IServiceRepository
    {
        private readonly WebDbContext _dbContext;
        public ServiceRepository(WebDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<List<Service>> GetServicesAsync()
        {
            // Adjust query according to your database structure
            return await _dbContext.Services.ToListAsync();
        }


    }
}
