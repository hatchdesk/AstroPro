using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Admin.Service
{
    public class ServiceToCreateViewModel
    {
        [Required(ErrorMessage ="Icon Url is Required")]
        public string? Icon { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is Required")]
        public string Content { get; set; } = string.Empty;

		[Required(ErrorMessage = "Content is Required")]
		public string FeeText { get; set; } = string.Empty;
	}

}
