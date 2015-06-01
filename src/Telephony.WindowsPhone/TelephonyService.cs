using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.Email;
using Windows.System;

namespace Telephony
{
    public class TelephonyService : ITelephonyService
    {
        public virtual async Task ComposeEmail(IEmailMessage emailMessage)
        {
            if (emailMessage == null)
            {
                throw new ArgumentNullException("emailMessage", "Supplied argument 'emailMessage' is null.");
            }

            if (!CanComposeEmail)
            {
                throw new FeatureNotAvailableException();
            }

            var email = new Windows.ApplicationModel.Email.EmailMessage()
            {

            };

 
            await EmailManager.ShowComposeNewEmailAsync(null);
        }

        public virtual async Task ComposeSMS(string recipient, string message = null)
        {
            if (!CanComposeSMS)
            {
                throw new FeatureNotAvailableException();
            }

            var sms = new ChatMessage
            {
                Recipients = {recipient},
                Body = message
            };

            await ChatMessageManager.ShowComposeSmsMessageAsync(sms);
        }

        public virtual Task MakePhoneCall(string recipient, string displayName = null)
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

            PhoneCallManager.ShowPhoneCallUI(recipient, displayName);

            return Task.FromResult(true);
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