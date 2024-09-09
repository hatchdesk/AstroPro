using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Fee;
using Web.Application.ViewModels.Admin.Page;

namespace Web.Application.Interfaces.Services
{
    public interface IFeeService
    {
        Task<List<FeeToViewModel>> GetAllFeeAsync();
		Task<FeeEditToViewModel?> GetFeesByServiceId(int id);
        Task<FeeToViewModel?> AddFeeAsync(FeeToCreateViewModel model);
        Task<FeeToViewModel?> UpdateFeeAsync(FeeEditToViewModel model);
        Task<bool> DeleteFeeAsync(int Id);
    }
}
