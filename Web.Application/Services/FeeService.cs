using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Fee;
using Web.Application.ViewModels.Admin.Page;
using Web.Domian.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Application.Services
{
    public class FeeService : IFeeService
    {
        private readonly IFeeRepositoary _feeRepositoary;

        private readonly IUnitOfWork _unitOfWork;

        public FeeService(IFeeRepositoary feeRepositoary, IUnitOfWork unitOfWork)
        {
            _feeRepositoary = feeRepositoary;
            _unitOfWork = unitOfWork;

        }

        public async Task<FeeToViewModel?> AddFeeAsync(FeeToCreateViewModel model)
        {
            var fee = new Fee()
            {
                Amount = model.Amount,
                ServiceName=model.ServiceName,
              

            };
            var addedpages = await _feeRepositoary.AddAsync(fee);
            var savedpages = await _unitOfWork.SaveChangesAsync();
            if (savedpages > 0)
            {
                return new FeeToViewModel
                {
                    Amount = fee.Amount,
                    ServiceName = fee.ServiceName,
                
                };

            }
            return null;
        }

        public async Task<bool> DeleteFeeAsync(int Id)
        {
            var isDeleted = await _feeRepositoary.DeleteAsync(Id);
            var deletedRecored = await _unitOfWork.SaveChangesAsync();
            if (deletedRecored > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<FeeToViewModel>> GetAllFeeAsync()
        {
            var ObjFees = await _feeRepositoary.GetAllAsync();
            return ObjFees.Select(e => new FeeToViewModel
            {
                Id = e.Id,
              ServiceName = e.ServiceName,
              Amount = e.Amount,
            
            }).ToList();
        }

		public async Task<FeeEditToViewModel?> GetFeesByServiceId(int id)
		{
            var fee = await _feeRepositoary.GetAsync(id);
			if (fee == null)
			{
				return null;
			}

			return new FeeEditToViewModel
            {
				Id = fee.Id,
				ServiceName = fee.ServiceName,
				Amount = fee.Amount,
            
			};
		}

        public async Task<FeeToViewModel?> UpdateFeeAsync(FeeEditToViewModel model)
        {

            var updated = await _feeRepositoary.GetAsync(model.Id);
            if (updated == null)
            {
                return new FeeToViewModel();
            }
            updated.Amount = model.Amount;
            updated.ServiceName = model.ServiceName;
        

            _feeRepositoary.UpdateAsync(updated);
            await _unitOfWork.SaveChangesAsync();

            return new FeeToViewModel
            {
                Amount = updated.Amount,
                ServiceName = updated.ServiceName,
             
            };
        }
    }
}