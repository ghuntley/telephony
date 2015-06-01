using System;
using System.Threading.Tasks;
using Windows.System;
using Microsoft.Phone.Tasks;

namespace Telephony
{
    public class TelephonyService : ITelephonyService
    {
        public virtual async Task ComposeEmail(IEmailMessage emailMessage)
        {
            if (emailMessage == null)
            {
                throw new ArgumentNullException("emailMessage",
                    "Supplied argument 'emailMessage' is null.");
            }

            if (!CanComposeEmail)
            {
                throw new FeatureNotAvailableException();
            }

            var task = new EmailComposeTask
            {
                To = emailMessage.To.ToString(),
                Cc = emailMessage.Cc.ToString(),
                Bcc = emailMessage.Bcc.ToString(),
                
                Subject = emailMessage.Subject,
                Body = emailMessage.Body
            };

            task.Show();
        }

        public virtual async Task ComposeSMS(string recipient, string message = null)
        {
            if (string.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentNullException("recipient",
                    "Supplied argument 'recipient' is null, whitespace or empty.");
            }

            if (!CanComposeSMS)
            {
                throw new FeatureNotAvailableException();
            }

            var task = new SmsComposeTask()
            {
                To = recipient,
                Body = message
            };

            task.Show();
        }

        public virtual async Task MakePhoneCall(string recipient, string displayName = null)
        {
            if (string.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentNullException("recipient",
                    "Supplied argument 'recipient' is null, whitespace or empty.");
            }

            if (!CanMakePhoneCall)
            {
                throw new FeatureNotAvailableException();
            }

            var task = new PhoneCallTask
            {
                PhoneNumber = recipient,
                DisplayName = displayName ?? recipient
            };

            task.Show();
        }

        public virtual async Task MakeVideoCall(string recipient, string displayName = null)
        {
            if (string.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentNullException("recipient",
                    "Supplied argument 'recipient' is null, whitespace or empty.");
            }

            if (!CanMakeVideoCall)
            {
                throw new FeatureNotAvailableException();
            }

            var uri = new Uri("skype://" + recipient + "?call");
            await Launcher.LaunchUriAsync(uri);
        }

        public virtual bool CanComposeEmail
        {
            get { return true; }
        }

        public virtual bool CanComposeSMS
        {
            get { return true; }
        }

        public virtual bool CanMakePhoneCall
        {
            get { return true; }
        }

        public virtual bool CanMakeVideoCall
        {
            get { return true; }
        }
    }
}