using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.Services;
using Web.Application.ViewModels.Admin.Articles;
using Web.Application.ViewModels.Admin.Fee;
using Web.Application.ViewModels.Admin.Page;

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

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var isDeleted = await _feeService.DeleteFeeAsync(Id);
            if (isDeleted)
            {
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeeToCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isAdded = await _feeService.AddFeeAsync(model);
                if (isAdded != null)
                {
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var feeWithServices = await _feeService.GetFeesByServiceId(Id);
            if (feeWithServices != null)
            {
                return View(feeWithServices);
            }
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Edit(FeeEditToViewModel model)
        {
            if (ModelState.IsValid)
            {

                var edited = await _feeService.UpdateFeeAsync(model);
                if (edited != null)
                {
                    return RedirectToAction("List");
                }
            }
            return View();

        }

    }
}
