using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookTour.Application.Interface;
using BookTour.Application.Dto;
using Microsoft.Extensions.Options;

namespace BookTour.Application.Service
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            var settings = emailSettings.Value;
            _smtpClient = new SmtpClient(settings.SmtpServer)
            {
                Port = settings.SmtpPort,
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                EnableSsl = true
            };
        }



        public async Task sendHtmlMessage(string to, string subject, string htmlBody)
        {
            try
            {
                // Tạo một đối tượng MailMessage mới
                var message = new MailMessage
                {
                    From = new MailAddress("nguyenhoangtuan12102003@gmail.com"),  // Địa chỉ email gửi
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true // Chỉ định đây là email HTML
                };

                message.To.Add(to);  // Thêm người nhận

                // Gửi email
                 _smtpClient.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to send email", e);
            }
        }
    }
}
