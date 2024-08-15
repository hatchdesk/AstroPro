using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Application.Interfaces.Services;
using Web.Application.Services;
using Web.Application.ViewModels.Admin.Page;
using Web.Application.ViewModels.Consultation;
using Web.Domian.Entities;

namespace Web.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IConsultationService _consultationService;
        private readonly IServiceService _serviceService; 
		private readonly IArticleService _articleService;

        public ConsultationController(IConsultationService consultationService, IServiceService serviceService , IArticleService articleService)
        {
            _consultationService = consultationService;
            _serviceService = serviceService;
			_articleService = articleService;
        }

        public async Task<IActionResult> Success()
        {
			var pageViewModel = await _articleService.GetHomePage();
			var HomeView = new PageParentModel()
			{
				ArticleToModel = pageViewModel!.ArticleToModel,
				ServiceToModel = pageViewModel!.ServiceToModel,
				PageModel = pageViewModel.PageModel,
				ConsultationToModel = pageViewModel.ConsultationToModel,
			};
			return View(HomeView);
        }

		[HttpPost]
		public async Task<IActionResult> SendEmail(PageParentModel pageParentModel, string CaptchaResponse)
		{
			if (!ModelState.IsValid)
			{
                ModelState.AddModelError("", "please verify captcha !");
                return RedirectToAction( "Page" ,  "Home", new { name = "consultation"});
			}

			if (string.IsNullOrEmpty(CaptchaResponse))
			{
				ModelState.AddModelError("", "CAPTCHA response is missing.");
                return RedirectToAction("Page", "Home", new { name = "consultation" });
            }

			if (!await VerifyCaptcha(CaptchaResponse))
			{
				ModelState.AddModelError("", "CAPTCHA verification failed. Please try again.");
                return RedirectToAction("Page", "Home", new { name = "consultation" });
            }
			var consultationModel = pageParentModel.ConsultationToModel;
            var consultmodel = pageParentModel.ServiceToModel;
          
           
			if (consultationModel != null)
			{

               
                string subject = "Request Consultation";
				string body = $@"
            <h3>Consultation Request Details</h3>
            <ul>
                <li><strong>Category:</strong> {consultationModel!.ServiceName}</li>
                <li><strong>Name:</strong> {consultationModel.Name}</li>
                <li><strong>Email:</strong> {consultationModel.Email}</li>
                <li><strong>Phone:</strong> {consultationModel.Phone}</li>
                <li><strong>Date of Birth:</strong> {consultationModel.DateOfBirth}</li>
                <li><strong>Time of Birth:</strong> {consultationModel.TimeOfBirth}</li>
                <li><strong>Place of Birth:</strong> {consultationModel.PlaceOfBirth}</li>
            </ul>
            <p><strong>Description:</strong><br/>{consultationModel.AnyInformation}</p>
        ";

				try
				{
					await _consultationService.SendEmailAsync(consultationModel.Email, subject, body);
					return RedirectToAction("Success");
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = $"Error sending consultation request: {ex.Message}";
					ModelState.AddModelError("", "An error occurred while sending the email. Please try again later.");
				}
			}
            return RedirectToAction("Page", "Home", new { name = "consultation" });
        }


		private async Task<bool> VerifyCaptcha(string captchaResponse)
        {
            var secretKey = "6Lfr5iIqAAAAAIyZwXKvllbhnNQAfgqTDNqJkp6c";
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponse>(jsonResponse);
                    return captchaResult?.Success ?? false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during CAPTCHA verification: {ex.Message}");
            }
            return false;
        }
    }
}