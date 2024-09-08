using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.ViewModels.Admin.Fee
{
    public class FeeToViewModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Muhurta Name")]
        public string? ServiceName { get; set; }

        [Required]
        [Display(Name = "Fee Amount")]
        public decimal Amount { get; set; }
    }
}
