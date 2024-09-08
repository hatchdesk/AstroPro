using Web.Application.Interfaces;
using Web.Application.Interfaces.Repositories;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Fee;
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

        public async Task<List<FeeToViewModel>> GetAllFeeAsync()
        {
            var ObjFees = await _feeRepositoary.GetAllAsync();
            return ObjFees.Select(e => new FeeToViewModel
            {
                Id = e.Id,
              ServiceName = e.ServiceName,
              Amount = e.Amount
            }).ToList();
        }

		public async Task<FeeToViewModel?> GetFeesByServiceId(int id)
		{
            var fee = await _feeRepositoary.GetAsync(id);
			if (fee == null)
			{
				return null;
			}

			return new FeeToViewModel
			{
				Id = fee.Id,
				ServiceName = fee.ServiceName,
				Amount = fee.Amount
			};
		}
	}
}