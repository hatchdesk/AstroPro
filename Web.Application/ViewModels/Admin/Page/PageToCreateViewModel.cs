using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Admin.Page
{
    public class PageToCreateViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tag is Required")]
        public string Tag { get; set; } = string.Empty;
    }
}
