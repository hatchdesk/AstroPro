using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.ViewModels.NewFolder
{
    public class CaptchaResponseToViewModel
    {
        [Required(ErrorMessage ="Captcha is not Success")]
        public bool Success { get; set; }
    }
}
