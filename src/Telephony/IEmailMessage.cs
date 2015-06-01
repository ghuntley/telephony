using System.Collections.Generic;

namespace Telephony
{
    public interface IEmailMessage
    {
        bool IsHTML { get; }
        string Body { get; }
        string Subject { get; }
        List<EmailRecipient> To { get; }
        List<EmailRecipient> Cc { get; }
        List<EmailRecipient> Bcc { get; }
    }
}