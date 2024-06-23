using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.EmailService
{
    public struct EmailConfig
    {
        public string SMTP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromAddress { get; set; }
        public string FriendlyName { get; set; }
    }

    public class EmailServiceLegacy
    {
        private readonly EmailConfig config;
        public EmailServiceLegacy(EmailConfig config)
        {
            this.config = config;
        }

        public void SendDefaultEmailWithAttachment(byte[] attachment, string fileName, string toAddress, string subject, string body)
        {
            using (MailMessage mail = new MailMessage(config.FromAddress, toAddress))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.Attachments.Add(new Attachment(new MemoryStream(attachment), fileName));
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = config.SMTP;
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(config.Username, config.Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
            }
        }
    }
}
