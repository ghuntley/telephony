using System;
using System.Collections.Generic;

namespace Telephony
{
    public class Email
    {
        public Email(string receipients, string subject = null, string body = null)
            : this()
        {
            if (string.IsNullOrWhiteSpace(receipients))
            {
                throw new ArgumentNullException("receipients",
                    "Supplied argument 'receipients' is null, whitespace or empty.");
            }

            To.Add(receipients);
            Subject = subject;
            Body = body;
        }

        public Email(IEnumerable<MailAddress> receipients, string subject = null, string body = null)
            : this()
        {
            if (receipients == null)
            {
                throw new ArgumentNullException("receipients", "Supplied argument 'receipients' is null.");
            }

            foreach (var mailAddress in receipients)
            {
                To.Add(mailAddress);
            }

            Subject = subject;
            Body = body;
        }

        protected Email()
        {
            To = new MailAddressCollection();
            Cc = new MailAddressCollection();
            Bcc = new MailAddressCollection();
            Attachments = new AttachmentCollection();
        }

        public AttachmentCollection Attachments { get; set; }

        public MailAddressCollection Bcc { get; set; }

        public string Body { get; set; }

        public MailAddressCollection Cc { get; set; }

        public bool IsHTML { get; set; }

        public string Subject { get; set; }

        public MailAddressCollection To { get; set; }
    }
}