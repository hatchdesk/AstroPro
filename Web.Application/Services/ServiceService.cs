﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Admin.PageContent;
using Web.Application.ViewModels.Admin.Service;
using Web.Domian.Entities;

namespace Web.Application.Services
{
   
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceService(IServiceRepository  serviceRepository , IUnitOfWork unitOfWork , IArticleRepository articleRepository , IPageRepository pageRepository)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
            _pageRepository = pageRepository;

            
        }

        public async Task<ServiceToViewModel?> AddServicesAsync(ServiceToCreateViewModel model)
        {
            var objServices = new Service()
            {
                Title = model.Title,
                FeeText = model.FeeText,
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
                FeeText = x.FeeText,
                Icon = x.Icon,
                Title = x.Title,
                Content = x.Content,
            }).ToList();
        }

        public async Task<ServiceToViewModel?> GetServiceAsync(int id)
        {
            var services = await _serviceRepository.GetAsync(id);
            if (services == null)
                return null;
            return new ServiceToViewModel()
            {
                Id = services.Id,
                Icon = services.Icon,
                FeeText = services.FeeText,
                Title = services.Title,
                Content = services.Content,
            };
        }
        public async Task<bool> DeleteServiceAsync(int id)
        {
            var services = await _serviceRepository.DeleteAsync(id);
            var savedServices = await _unitOfWork.SaveChangesAsync();
            if(savedServices > 0)
            {
                return true;
            }
            return false;
            
        }

        public async Task<ServiceToCreateViewModel?> UpdateServiceAsync(ServiceToEditViewModel model)
        {
            var Updated = await _serviceRepository.GetAsync(model.Id);
            if (Updated == null)
            {
                return new ServiceToCreateViewModel();
            }


            Updated.Title = model.Title;
            Updated.Content = model.Content;
            Updated.Icon = model.Icon;
            Updated.FeeText = model.FeeText;

              _serviceRepository.UpdateAsync(Updated);
            await _unitOfWork.SaveChangesAsync();

            return new ServiceToCreateViewModel()
            {
               
                Title = Updated.Title,
                Content = Updated.Content,
                Icon = Updated.Icon,
                FeeText = Updated.FeeText,
            };
        }

    }
}
