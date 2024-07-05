using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Admin.Account
{
    public  class LogInToCreateViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = string.Empty;
    }
}
