using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Web.Application.Interfaces.Services;
using Web.Application.Options;
using Web.Application.ViewModels.Consultation;

namespace Web.Application.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly EmailSettingsOption _settings;

        public ConsultationService(IOptions<EmailSettingsOption> emailSettings)
        {
            _settings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string Email ,string subject ,  string body)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(_settings.SmtpServer))
                {
                    var adminEmail = _settings.From;
                    smtpClient.Port = _settings.SmtpPort;
                    smtpClient.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_settings.Username);
                        mailMessage.To.Add(adminEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        await smtpClient.SendMailAsync(mailMessage);
                    }

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_settings.Username);
                        mailMessage.To.Add(Email);
                        mailMessage.Subject = subject;
                        mailMessage.Body = "Hi, Thank you for reaching us. <br/> We will contact you back soon!";
                        mailMessage.IsBodyHtml = true;

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error sending email: {ex.Message}", ex);
            }
        }
    }
}
