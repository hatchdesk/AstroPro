using Microsoft.AspNetCore.Mvc;
using Web.Application.ViewModels.Admin.Service;

namespace Web.Application.Interfaces.Services
{
    public  interface IServiceService 
    {
        public Task<List<ServiceToViewModel>> GetAllServiceAsync();
        public Task<ServiceToViewModel?> AddServicesAsync(ServiceToCreateViewModel model);
        public Task<ServiceToViewModel?> GetServiceAsync(int id);
        public Task<bool> DeleteServiceAsync(int id);
        public Task<ServiceToCreateViewModel?> UpdateServiceAsync(ServiceToEditViewModel model);

    }
}
