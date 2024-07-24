using Microsoft.AspNetCore.Mvc;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Consultation;


namespace Web.Controllers
{
	public class ConsultationController : Controller
	{
		private readonly IConsultationService _consultationService;
		private readonly IServiceService _serviceService;

		public ConsultationController(IConsultationService consultationService, IServiceService serviceService)
		{
			_consultationService = consultationService;
			_serviceService = serviceService;
		}

        [HttpPost]
        public async Task<IActionResult> SendEmail(PageParentModel pageParentModel)
        {
            if (ModelState.IsValid)
            {

                var consultationModel = pageParentModel?.ConsultationToModel;
                if (consultationModel != null)
                {
                    string subject = "Request Consultation";
                    string body = $@"
                <h3>Consultation Request Details</h3>
                <ul>
                    <li><strong>Name:</strong> {consultationModel.Name}</li>
                    <li><strong>Email:</strong> {consultationModel.Email}</li>
                    <li><strong>Phone:</strong> {consultationModel.Phone}</li>
                    <li><strong>Date of Birth:</strong> {consultationModel.DateOfBirth}</li>
                    <li><strong>Place of Birth:</strong> {consultationModel.PlaceOfBirth}</li>
                </ul>
                <p><strong>Description:</strong><br/>{consultationModel.AnyInformation}</p>
            ";

                    try
                    {
                        await _consultationService.SendEmailAsync(consultationModel.Email, subject, body);
                        return RedirectToAction("Page", "Home", new { name = "Consultation" });
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"Error sending consultation request: {ex.Message}";
                    }

                }

                TempData["ErrorMessage"] = "Error sending consultation request, Please try again.";
                return View("Consultation", consultationModel);
            }
            return View("Consultation" , pageParentModel.ConsultationToModel);
           
        }

    }
}



