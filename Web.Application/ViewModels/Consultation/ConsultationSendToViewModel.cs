using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Application.ViewModels.Consultation
{
    public class ConsultationSendToViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is Required")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of Birth is Required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

		[Required(ErrorMessage = "Time of Birth is Required")]
		[DataType(DataType.Time)]
		public TimeOnly TimeOfBirth { get; set; }

		[Required(ErrorMessage = "Place of Birth is Required")]
        public string PlaceOfBirth { get; set; } = string.Empty;

        public string ? AnyInformation { get; set; }
    }
}
