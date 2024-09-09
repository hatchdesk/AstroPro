using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Admin.Fee
{
    public  class FeeToCreateViewModel
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
