using System;
using System.Text;
using Foundation;
using MessageUI;
using UIKit;


namespace Telephony
{
    public class TelephonyService : ITelephonyService
    {
        public virtual void ComposeEmail(string to, string subject, string body)
        {
            const bool isHtml = false;
            var mailer = new MFMailComposeViewController();
            mailer.SetMessageBody(body ?? string.Empty, isHtml);
            mailer.SetSubject(subject ?? string.Empty);
            //mailer.SetCcRecipients(cc);
            mailer.SetToRecipients(new[] { to });
            mailer.Finished += (s, e) =>
            {
                ((MFMailComposeViewController)s).DismissViewController(true, () =>
                {
                });
            };
        }

        private static string CleanPhoneNumber(string phonenumber)
        {
            if (String.IsNullOrWhiteSpace(phonenumber))
            {
                return String.Empty;
            }

            return phonenumber.Replace(" ", String.Empty);
        }

        public virtual void ComposeSms(string phoneNumber, string message = "")
        {
            var url = new NSUrl(String.Format("sms://{0}?body=, {1}", CleanPhoneNumber(phoneNumber), message));
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public virtual void MakeVideoCall(string name, string displayName)
        {
            var url = new NSUrl("facetime://" + displayName.Replace(" ", String.Empty));
            UIApplication.SharedApplication.OpenUrl(url);
        }

        public virtual void MakePhoneCall(string name, string number)
        {
            var url = new NSUrl("tel:" + number);
            UIApplication.SharedApplication.OpenUrl(url);
        }
    }
}
