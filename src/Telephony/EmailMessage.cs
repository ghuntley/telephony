using System.Collections.Generic;

namespace Telephony
{
    public class EmailMessage : IEmailMessage
    {
        protected EmailMessage()
        {
            To = new List<EmailRecipient>();
            Cc = new List<EmailRecipient>();
            Bcc = new List<EmailRecipient>();

            Subject = string.Empty;
            Body = string.Empty;
        }

        public List<EmailRecipient> Bcc { get; set; }
        public string Body { get; set; }
        public List<EmailRecipient> Cc { get; set; }
        public bool IsHTML { get; set; }
        public string Subject { get; set; }
        public List<EmailRecipient> To { get; set; }
    }
}