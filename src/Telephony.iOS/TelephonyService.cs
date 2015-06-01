using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using MessageUI;
using UIKit;

namespace Telephony
{
    public class TelephonyService : ITelephonyService
    {
        public virtual bool CanComposeEmail
        {
            get { return MFMailComposeViewController.CanSendMail; }
        }

        public virtual bool CanComposeSMS
        {
            get { return MFMessageComposeViewController.CanSendText; }
        }

        public virtual bool CanMakePhoneCall
        {
            get { return UIApplication.SharedApplication.CanOpenUrl(new NSUrl("tel://")); }
        }

        public virtual bool CanMakeVideoCall
        {
            get { return UIApplication.SharedApplication.CanOpenUrl(new NSUrl("facetime://")); }
        }

        public virtual Task ComposeEmail(Email email)
        {
            if (!CanComposeEmail)
            {
                throw new FeatureNotAvailableException();
            }

            var mailer = new MFMailComposeViewController();

            mailer.SetToRecipients(email.To.Select(x => x.Address).ToArray());
            mailer.SetCcRecipients(email.Cc.Select(x => x.Address).ToArray());
            mailer.SetBccRecipients(email.Bcc.Select(x => x.Address).ToArray());

            mailer.SetSubject(email.Subject ?? string.Empty);
            mailer.SetMessageBody(email.Body ?? string.Empty, email.IsHTML);

            mailer.Finished += (s, e) => ((MFMailComposeViewController) s).DismissViewController(true, () => { });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailer, true, null);

            return Task.FromResult(true);
        }

        public virtual Task ComposeSMS(string recipient, string message = null)
        {
            if (!CanComposeSMS)
            {
                throw new FeatureNotAvailableException();
            }

            var mailer = new MFMessageComposeViewController();
            mailer.Recipients = new[] {recipient};
            mailer.Body = message ?? string.Empty;

            mailer.Finished += (s, e) => ((MFMessageComposeViewController) s).DismissViewController(true, () => { });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailer, true, null);

            return Task.FromResult(true);
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

            var url = new NSUrl("tel:" + RemoveWhitespace(recipient));
            UIApplication.SharedApplication.OpenUrl(url);

            return Task.FromResult(true);
        }

        public virtual Task MakeVideoCall(string recipient, string displayName = null)
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

            var url = new NSUrl("facetime://" + RemoveWhitespace(recipient));
            UIApplication.SharedApplication.OpenUrl(url);

            return Task.FromResult(true);
        }

        /// <remarks>
        ///     NSUrl("[facetime://|tel://]") fails to function if there are spaces in the url.
        /// </remarks>
        private static string RemoveWhitespace(string phonenumber)
        {
            if (string.IsNullOrWhiteSpace(phonenumber))
            {
                return string.Empty;
            }

            return phonenumber.Replace(" ", string.Empty);
        }
    }
}