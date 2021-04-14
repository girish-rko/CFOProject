using Cfo.Business.DTO;
using Cfo.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.Services
{
    public class EmailService : ICfoMailerService
    {
        /// <summary>
        /// Calls the Async method wrapped in a Task.Run.
        /// </summary>
        /// <param name="sendEmailDto"></param>
        /// <returns></returns>
        public bool Send(SendEmailDto sendEmailDto, bool isHtml)
        {
            Task.Run(() => SendAsync(sendEmailDto, isHtml));
            return true;
        }

        public async Task<bool> SendAsync(SendEmailDto sendEmailDto, bool isHtml)
        {
            // ReSharper disable StringLiteralTypo
            var mailClient = new SmtpClient(ConfigurationManager.AppSettings["AWSSMTPHost"], Convert.ToInt32(ConfigurationManager.AppSettings["AWSSMTPPort"]))
            {
                Credentials =
                    new NetworkCredential(ConfigurationManager.AppSettings["AWSSMTPUser"],
                        ConfigurationManager.AppSettings["AWSSMTPPass"]),
                EnableSsl = true
            };
            // ReSharper restore StringLiteralTypo


            mailClient.EnableSsl = false;

            var message = new MailMessage(sendEmailDto.From, sendEmailDto.To)
            {
                Subject = sendEmailDto.Subject,
                Body = sendEmailDto.Body,
                IsBodyHtml = isHtml
            };
            if (sendEmailDto.Attachments.Count > 0)
            {
                foreach (var attach in sendEmailDto.Attachments)
                {
                    message.Attachments.Add(attach);
                }
            }
            await mailClient.SendMailAsync(message);

            return true;
        }
    }
}
