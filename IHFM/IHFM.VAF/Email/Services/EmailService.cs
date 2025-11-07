using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Email.Services
{
    internal class EmailService
    {
        private Configuration _configuration;

        public EmailService(Configuration configuration)
        {
            _configuration = configuration;
        }

        #region Public Email Methods

        public void SendEmailWithCalendarInvite(string toAddress, string subject, string body, string location, DateTime startTime, DateTime endTime)
        {
            using (MailMessage mail = new MailMessage(_configuration.Email_FromAddress, toAddress))
            {
                mail.Subject = subject;

                // Add calendar invite to email
                AddCalendarInviteToEmail(mail, subject, body, location, startTime, endTime, toAddress);

                SendEmail(mail);
            }
        }

        public void SendEmailWithAttachment(string toAddress, string subject, string body, byte[] attachment, string fileName)
        {
            using (MailMessage mail = new MailMessage(_configuration.Email_FromAddress, toAddress))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                // Add attachment to email
                mail.Attachments.Add(new Attachment(new MemoryStream(attachment), fileName));

                SendEmail(mail);
            }
        }

        public void SendSimpleEmail(string toAddress, string subject, string body, bool isHtml = false)
        {
            using (MailMessage mail = new MailMessage(_configuration.Email_FromAddress, toAddress))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isHtml;

                SendEmail(mail);
            }
        }

        #endregion

        #region Private Helper Methods

        private void AddCalendarInviteToEmail(MailMessage mail, string summary, string description, string location, DateTime startTime, DateTime endTime, string attendeeEmail)
        {
            // Create the calendar entry
            byte[] calendarData = CreateCalendarEntry(summary, description, location, startTime, endTime, attendeeEmail);

            // Create AlternateView with proper MIME type for calendar
            ContentType calendarType = new ContentType("text/calendar");
            calendarType.Parameters.Add("method", "REQUEST");
            calendarType.Parameters.Add("name", "invite.ics");

            AlternateView calendarView = AlternateView.CreateAlternateViewFromString(
                Encoding.UTF8.GetString(calendarData),
                calendarType
            );
            calendarView.TransferEncoding = TransferEncoding.Base64;
            mail.AlternateViews.Add(calendarView);
        }

        private void SendEmail(MailMessage mail)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    //if (!string.IsNullOrEmpty(_configuration.Email_PickupDirectoryLocation))
                    //{
                    //    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    //    smtp.PickupDirectoryLocation = _configuration.Email_PickupDirectoryLocation;
                    //}
                    //else
                    //{
                        // Production mode - uses SMTP server
                        smtp.Host = _configuration.Email_SMTP;
                        smtp.Port = _configuration.Email_Port;
                        smtp.EnableSsl = _configuration.Email_EnableSsl;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;

                        if (!string.IsNullOrEmpty(_configuration.Email_Username))
                        {
                            smtp.Credentials = new NetworkCredential(_configuration.Email_Username, _configuration.Email_Password);
                        }
                    //}

                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email: " + ex.Message, ex);
            }
        }

        private byte[] CreateCalendarEntry(string summary, string description, string location, DateTime startTime, DateTime endTime, string attendeeEmail)
        {
            StringBuilder calendar = new StringBuilder();

            // iCalendar header
            calendar.AppendLine("BEGIN:VCALENDAR");
            calendar.AppendLine("VERSION:2.0");
            calendar.AppendLine("PRODID:-//IHFM//VAF Calendar//EN");
            calendar.AppendLine("METHOD:REQUEST");
            calendar.AppendLine("CALSCALE:GREGORIAN");

            // Event details
            calendar.AppendLine("BEGIN:VEVENT");

            // Unique ID for the event
            string uid = Guid.NewGuid().ToString();
            calendar.AppendLine($"UID:{uid}");

            // Date/Time stamp (current time in UTC)
            string dateStamp = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ");
            calendar.AppendLine($"DTSTAMP:{dateStamp}");

            // Start and end times (in UTC)
            string startUtc = startTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
            string endUtc = endTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
            calendar.AppendLine($"DTSTART:{startUtc}");
            calendar.AppendLine($"DTEND:{endUtc}");

            // Summary (title)
            calendar.AppendLine($"SUMMARY:{EscapeCalendarText(summary)}");

            // Description
            if (!string.IsNullOrEmpty(description))
            {
                calendar.AppendLine($"DESCRIPTION:{EscapeCalendarText(description)}");
            }

            // Location
            if (!string.IsNullOrEmpty(location))
            {
                calendar.AppendLine($"LOCATION:{EscapeCalendarText(location)}");
            }

            // Organizer
            calendar.AppendLine($"ORGANIZER:mailto:{_configuration.Email_FromAddress}");

            // Attendee
            calendar.AppendLine($"ATTENDEE;RSVP=TRUE;ROLE=REQ-PARTICIPANT:mailto:{attendeeEmail}");

            // Status
            calendar.AppendLine("STATUS:CONFIRMED");
            calendar.AppendLine("SEQUENCE:0");

            // Priority (1-9, 1 is highest)
            calendar.AppendLine("PRIORITY:5");

            calendar.AppendLine("END:VEVENT");
            calendar.AppendLine("END:VCALENDAR");

            return Encoding.UTF8.GetBytes(calendar.ToString());
        }

        private string EscapeCalendarText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Escape special characters according to RFC 5545
            return text
                .Replace("\\", "\\\\")
                .Replace(",", "\\,")
                .Replace(";", "\\;")
                .Replace("\n", "\\n")
                .Replace("\r", "");
        }

        #endregion
    }
}
