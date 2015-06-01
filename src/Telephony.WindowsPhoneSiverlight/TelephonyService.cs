using System;
using System.Threading.Tasks;
using Windows.System;
using Microsoft.Phone.Tasks;

namespace Telephony
{
    public class TelephonyService : ITelephonyService
    {
        public virtual Task ComposeEmail(Email email)
        {
            if (!CanComposeEmail)
            {
                throw new FeatureNotAvailableException();
            }

            throw new NotImplementedException();
        }

        public virtual async Task ComposeSMS(string recipient, string message = null)
        {
            if (!CanComposeSMS)
            {
                throw new FeatureNotAvailableException();
            }

            Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
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

            var task = new PhoneCallTask
            {
                PhoneNumber = recipient,
                displayName = displayName ?? recipient
            };

            phoneCallTask.Show();
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