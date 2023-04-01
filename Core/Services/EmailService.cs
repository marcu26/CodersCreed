using ISoft.Travel.Core.Dtos.EmailDtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISoft.Travel.Core.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        private EmailConfigDto getEmailConfigurations()
        {
            var EmailConfig = _config.GetSection("EmailConfig");

            return new EmailConfigDto()
            {
                EmailHost = EmailConfig.GetValue<string>("EmailHost"),
                EmailPassword = EmailConfig.GetValue<string>("EmailPassword"),
                EmailUserName = EmailConfig.GetValue<string>("EmailUserName"),
                Port = EmailConfig.GetValue<int>("Port")
            };
        }

        public async Task sendEmailAsync(EmailDto request)
        {
            var EmailConfig = getEmailConfigurations();

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(EmailConfig.EmailUserName));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = request.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(EmailConfig.EmailHost, EmailConfig.Port, SecureSocketOptions.StartTlsWhenAvailable);
            await smtp.AuthenticateAsync(EmailConfig.EmailUserName, EmailConfig.EmailPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
