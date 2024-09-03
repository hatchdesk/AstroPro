using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class FeeController : Controller
    {
        private readonly IFeeService _feeService;
        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var fees = await _feeService.GetAllFeeAsync();
            return View(fees);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CalculateFee()
        {
            var fee = await _feeService.GetFeesByServiceId(2);
            if (fee == null)
            {
				return Json(new { Amount = 0 });
			}
		    return Json(fee);
		}
    }
}
