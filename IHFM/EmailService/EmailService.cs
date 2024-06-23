//using MailKit.Net.Smtp;
//using MimeKit;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IHFM.EmailService
//{
    
//    public class EmailServiceClass
//    {
//        private readonly EmailConfig config;
//        public EmailServiceClass(EmailConfig config)
//        {
//            this.config = config;
//        }

//        public void SendDefaultEmailWithAttachment(byte[] attachment, string fileName, string toAddress, string subject, string body)
//        {
//            var message = new MimeMessage();
//            message.From.Add(new MailboxAddress(config.FriendlyName, config.FromAddress));

//            string[] addys = toAddress.Split(new char[] { ';' });

//            foreach (string addy in addys)
//            {
//                message.To.Add(new MailboxAddress("", addy));
//            }

//            message.Subject = subject;

//            var emailBody = new BodyBuilder
//            {
//                HtmlBody = body
//            };
//            emailBody.Attachments.Add(fileName, attachment);

//            message.Body = emailBody.ToMessageBody();
//            using (var client = new SmtpClient())
//            {
//                client.Connect(config.SMTP, 465, true);
//                client.Authenticate(config.Username, config.Password);
//                client.Send(message);
//                client.Disconnect(true);
//            }
//        }
//    }
//}
