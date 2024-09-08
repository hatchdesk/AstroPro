using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Fee;

namespace Web.Application.Interfaces.Services
{
    public interface IFeeService
    {
        Task<List<FeeToViewModel>> GetAllFeeAsync();
		Task<FeeToViewModel?> GetFeesByServiceId(int id);
	}
}
