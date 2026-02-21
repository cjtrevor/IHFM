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
    /// <summary>
    /// Defines the recurrence frequency for calendar events
    /// </summary>
    public enum RecurrenceFrequency
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    /// <summary>
    /// Represents a recurrence pattern for calendar events
    /// </summary>
    public class RecurrencePattern
    {
        /// <summary>
        /// Frequency of recurrence (Daily, Weekly, Monthly, Yearly)
        /// </summary>
        public RecurrenceFrequency Frequency { get; set; }

        /// <summary>
        /// Interval between recurrences (e.g., every 2 weeks = Interval: 2)
        /// </summary>
        public int Interval { get; set; } = 1;

        /// <summary>
        /// Days of the week for weekly recurrence (e.g., Sunday, Wednesday, Thursday)
        /// Use DayOfWeek enum values
        /// </summary>
        public List<DayOfWeek> DaysOfWeek { get; set; }

        /// <summary>
        /// Day of month for monthly recurrence (1-31)
        /// </summary>
        public int? DayOfMonth { get; set; }

        /// <summary>
        /// Number of occurrences before ending (optional)
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// End date for recurrence (optional)
        /// </summary>
        public DateTime? Until { get; set; }

        public RecurrencePattern()
        {
            Frequency = RecurrenceFrequency.None;
            DaysOfWeek = new List<DayOfWeek>();
        }
    }

    internal class EmailService
    {
        private Configuration _configuration;

        public EmailService(Configuration configuration)
        {
            _configuration = configuration;
        }

        #region Public Email Methods

        public void SendEmailWithCalendarInvite(string toAddress, string subject, string body, string location, DateTime startTime, DateTime endTime, RecurrencePattern recurrence = null)
        {
            using (MailMessage mail = new MailMessage(_configuration.Email_FromAddress, toAddress))
            {
                mail.Subject = subject;

                // Add calendar invite to email
                AddCalendarInviteToEmail(mail, subject, body, location, startTime, endTime, toAddress, recurrence);

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

        private void AddCalendarInviteToEmail(MailMessage mail, string summary, string description, string location, DateTime startTime, DateTime endTime, string attendeeEmail, RecurrencePattern recurrence = null)
        {
            // Create the calendar entry
            string calendarString = Encoding.UTF8.GetString(CreateCalendarEntry(summary, description, location, startTime, endTime, attendeeEmail, recurrence));

            // Add plain text view (required for Outlook)
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(
                mail.Body ?? description,
                Encoding.UTF8,
                MediaTypeNames.Text.Plain
            );
            mail.AlternateViews.Add(plainView);

            // Add calendar view with proper ContentType
            ContentType calendarType = new ContentType("text/calendar");
            calendarType.Parameters.Add("method", "REQUEST");
            calendarType.Parameters.Add("charset", "UTF-8");

            AlternateView calendarView = AlternateView.CreateAlternateViewFromString(
                calendarString,
                calendarType
            );
            mail.AlternateViews.Add(calendarView);

            // Also attach .ics file for better Outlook compatibility
            Attachment icsAttachment = Attachment.CreateAttachmentFromString(calendarString, "invite.ics");
            icsAttachment.ContentType = new ContentType("text/calendar; method=REQUEST; charset=UTF-8");
            mail.Attachments.Add(icsAttachment);
        }

        private void SendEmail(MailMessage mail)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = _configuration.Email_SMTP;
                    smtp.Port = _configuration.Email_Port;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;

                    if (!string.IsNullOrEmpty(_configuration.Email_Username))
                    {
                        smtp.Credentials = new NetworkCredential(_configuration.Email_Username, _configuration.Email_Password);
                    }

                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to send email: " + ex.Message, ex);
            }
        }

        private byte[] CreateCalendarEntry(string summary, string description, string location, DateTime startTime, DateTime endTime, string attendeeEmail, RecurrencePattern recurrence = null)
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

            // Add recurrence rule if specified
            if (recurrence != null && recurrence.Frequency != RecurrenceFrequency.None)
            {
                string rrule = BuildRecurrenceRule(recurrence);
                if (!string.IsNullOrEmpty(rrule))
                {
                    calendar.AppendLine($"RRULE:{rrule}");
                }
            }

            // Status
            calendar.AppendLine("STATUS:CONFIRMED");
            calendar.AppendLine("SEQUENCE:0");

            // Priority (1-9, 1 is highest)
            calendar.AppendLine("PRIORITY:5");

            calendar.AppendLine("END:VEVENT");
            calendar.AppendLine("END:VCALENDAR");

            return Encoding.UTF8.GetBytes(calendar.ToString());
        }

        private string BuildRecurrenceRule(RecurrencePattern recurrence)
        {
            if (recurrence == null || recurrence.Frequency == RecurrenceFrequency.None)
                return string.Empty;

            StringBuilder rrule = new StringBuilder();
            
            // Add frequency
            rrule.Append($"FREQ={recurrence.Frequency.ToString().ToUpper()}");

            // Add interval if not 1
            if (recurrence.Interval > 1)
            {
                rrule.Append($";INTERVAL={recurrence.Interval}");
            }

            // Add day of week for weekly recurrence
            if (recurrence.Frequency == RecurrenceFrequency.Weekly && recurrence.DaysOfWeek != null && recurrence.DaysOfWeek.Count > 0)
            {
                var days = recurrence.DaysOfWeek.Select(d => ConvertDayOfWeekToICalFormat(d));
                rrule.Append($";BYDAY={string.Join(",", days)}");
            }

            // Add day of month for monthly recurrence
            if (recurrence.Frequency == RecurrenceFrequency.Monthly && recurrence.DayOfMonth.HasValue)
            {
                rrule.Append($";BYMONTHDAY={recurrence.DayOfMonth.Value}");
            }

            // Add count or until date
            if (recurrence.Count.HasValue)
            {
                rrule.Append($";COUNT={recurrence.Count.Value}");
            }
            else if (recurrence.Until.HasValue)
            {
                string untilDate = recurrence.Until.Value.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
                rrule.Append($";UNTIL={untilDate}");
            }

            return rrule.ToString();
        }

        private string ConvertDayOfWeekToICalFormat(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return "SU";
                case DayOfWeek.Monday:
                    return "MO";
                case DayOfWeek.Tuesday:
                    return "TU";
                case DayOfWeek.Wednesday:
                    return "WE";
                case DayOfWeek.Thursday:
                    return "TH";
                case DayOfWeek.Friday:
                    return "FR";
                case DayOfWeek.Saturday:
                    return "SA";
                default:
                    return string.Empty;
            }
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
