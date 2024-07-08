using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Application.Interfaces.Services;
using Web.Application.ViewModels.Consultation;

namespace Web.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IConsultationService _consultationService;

        public ConsultationController(IConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

        [HttpGet]
        public IActionResult Consultation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ConsultationSendToViewModel model)
        {
            if (ModelState.IsValid)
            {
                string subject = "Request Consultation";
                string body = $@"
                    <h3>Consultation Request Details</h3>
                    <ul>
                        <li><strong>Name:</strong> {model.Name}</li>
                        <li><strong>Email:</strong> {model.Email}</li>
                        <li><strong>Phone:</strong> {model.Phone}</li>
                        <li><strong>Date of Birth:</strong> {model.DateOfBirth}</li>
                        <li><strong>Place of Birth:</strong> {model.PlaceOfBirth}</li>
                    </ul>
                    <p><strong>Description:</strong><br/>{model.AnyInformation}</p>
                ";

                try
                {
                    await _consultationService.SendEmailAsync(model.Email, subject, body);
                     return RedirectToAction("Consultation");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Error sending consultation request: {ex.Message}";
                }

            
            }
                return View("Consultation", model);

          
        }
    }
}
