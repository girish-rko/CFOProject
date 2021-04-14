using Cfo.Business.DTO;
using Cfo.Business.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CFOProject.Services
{
    //Question: we also have an email service in the business project. Do we need both?
    public class EmailService : IIdentityMessageService
    {
        private readonly ICfoMailerService _cfoMailerService;

        public EmailService(ICfoMailerService cfoMailerService)
        {
            _cfoMailerService = cfoMailerService;
        }
        public async Task SendAsync(IdentityMessage message)
        {
            await _cfoMailerService.SendAsync(new SendEmailDto
            {
                Body = message.Body,
                From = SendEmailDto.NoReply,
                Subject = message.Subject,
                To = new MailAddress(message.Destination)
            }, true);
        }
    }
}