using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.IO;

using Foundation;
using MessageUI;
using UIKit;

namespace Telephony
{
    public class TelephonyService : ITelephonyService
    {

        public Task ComposeEmail(Email email)
        {
            var mailer = new MFMailComposeViewController();

            mailer.SetToRecipients(email.To.ToArray());
            mailer.SetCcRecipients(email.Cc.ToArray());
            mailer.SetBccRecipients(email.Bcc.ToArray());

            mailer.SetSubject(email.Subject ?? string.Empty);
            mailer.SetMessageBody(email.Body ?? string.Empty, email.IsHTML);
            
            mailer.Finished += (s, e) => ((MFMailComposeViewController)s).DismissViewController(true, () =>
            {
            });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailer, true, null);
            
            return null;
        }

        public Task ComposeSMS(string recipient, string message = null)
        {
            throw new NotImplementedException();
        }

        public Task MakeVideoCall(string recipient, string displayName = null)
        {
            throw new NotImplementedException();
        }

        public Task MakePhoneCall(string recipient, string displayName = null)
        {
            throw new NotImplementedException();
        }

        public bool ComposeEmailFeatureEnabled
        {
            get
            {
                return MFMailComposeViewController.CanSendMail;
            }
        }

        public bool ComposeSMSFeatureEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool MakeVideoCallFeatureEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool MakePhoneCallFeatureEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

