using Cfo.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.Services.Interfaces
{
    public interface ICfoMailerService
    {
        bool Send(SendEmailDto sendEmailDto, bool isHtml);

        Task<bool> SendAsync(SendEmailDto sendEmailDto, bool isHtml);
    }

}
