using System;
using System.Collections.Generic;

namespace Telephony
{
    public class Email
    {
        public Email()
        {
            To = new MailAddressCollection();
            Cc = new MailAddressCollection();
            Bcc = new MailAddressCollection();
            Attachments = new AttachmentCollection();
        }

        public Email(string receipients, string subject = null, string body = null)
        {
            if (String.IsNullOrWhiteSpace(receipients))
            {
                throw new ArgumentNullException("receipients", "Supplied argument 'receipients' is null.");
            }

            To.Add(receipients);
            Subject = subject;
            Body = body;

            Cc = new MailAddressCollection();
            Bcc = new MailAddressCollection();
            Attachments = new AttachmentCollection();
        }

        public Email(IEnumerable<MailAddress> receipients, string subject = null, string body = null)
        {
            if (receipients == null)
            {
                throw new ArgumentNullException("receipients", "Supplied argument 'receipients' is null.");
            }

            foreach (MailAddress mailAddress in receipients)
            {
                To.Add(mailAddress);
            }

            Subject = subject;
            Body = body;

            Cc = new MailAddressCollection();
            Bcc = new MailAddressCollection();
            Attachments = new AttachmentCollection();
        }

        public MailAddressCollection To { get; set; }
        public MailAddressCollection Cc { get; set; }
        public MailAddressCollection Bcc { get; set; }

        public AttachmentCollection Attachments { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}