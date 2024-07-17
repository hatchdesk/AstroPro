using Web.Application.Interfaces.Repositories.Base;
using Web.Domian.Entities;

namespace Web.Application.Interfaces.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<List<Service>> GetServicesAsync();


    }
}
