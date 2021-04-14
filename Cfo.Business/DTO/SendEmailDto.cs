using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cfo.Business.DTO
{
    public class SendEmailDto
    {
        public MailAddress To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public MailAddress From { get; set; } = NoReply;

        public List<System.Net.Mail.Attachment> Attachments = new List<System.Net.Mail.Attachment>();

        public static MailAddress NoReply => new MailAddress("noreply@tronixtrm.com", "Tronix");

        public static MailAddress Support => new MailAddress("support@tronixtrm.com", "Tronix Support");
    }
}
