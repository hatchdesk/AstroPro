using Web.Application.ViewModels.Admin.Service;

namespace Web.Application.Interfaces.Services
{
    public  interface IServiceService 
    {
        public Task<List<ServiceToViewModel>> GetAllServiceAsync();
        public Task<ServiceToViewModel?> AddServicesAsync(ServiceToCreateViewModel model);

    }
}
