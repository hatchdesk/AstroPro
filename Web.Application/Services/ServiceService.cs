using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.Service;
using Web.Domian.Entities;

namespace Web.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceService(IServiceRepository  serviceRepository , IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;

            
        }

        public async Task<ServiceToViewModel?> AddServicesAsync(ServiceToCreateViewModel model)
        {
            var objServices = new Service()
            {
                Title = model.Title,
                Content = model.Content,
                Icon = model.Icon,
            };
            var addedService = await _serviceRepository.AddAsync(objServices);
           var SavedService =  await _unitOfWork.SaveChangesAsync();
            if(SavedService > 0)
            {
                return new ServiceToViewModel
                {
                    Id = addedService.Id,
                };
            }
            return null;

        }

        public async Task<List<ServiceToViewModel>> GetAllServiceAsync()
        {
           var services  = await _serviceRepository.GetAllAsync();
            return services.Select(x => new ServiceToViewModel
            {
                Id = x.Id,
                Icon = x.Icon,
                Title = x.Title,
                Content = x.Content,
            }).ToList();
        }
    }
}
