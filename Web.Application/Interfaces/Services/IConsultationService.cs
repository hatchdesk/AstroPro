using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.ViewModels.Consultation;

namespace Web.Application.Interfaces.Services
{
    public interface IConsultationService
    {

        Task SendEmailAsync(string Email, string subject, string body);
    }
}
