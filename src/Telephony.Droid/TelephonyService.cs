using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Text;
using Uri = Android.Net.Uri;

namespace Telephony
{
    public class TelephonyService : IntentService, ITelephonyService
    {
        /// <summary>
        ///     TODO: Determine appropriate way to toggle this on and off.
        /// </summary>
        public virtual bool CanComposeEmail
        {
            get { return true; }
        }

        /// <summary>
        ///     TODO: Determine appropriate way to toggle this on and off.
        /// </summary>
        public virtual bool CanComposeSMS
        {
            get { return true; }
        }

        /// <summary>
        ///     TODO: Determine appropriate way to toggle this on and off.
        /// </summary>
        public virtual bool CanMakePhoneCall
        {
            get { return true; }
        }

        /// <summary>
        ///     TODO: Determine appropriate way to toggle this on and off.
        /// </summary>
        public virtual bool CanMakeVideoCall
        {
            get { return true; }
        }

        public virtual Task ComposeEmail(Email email)
        {
            if (!CanComposeEmail)
            {
                throw new FeatureNotAvailableException();
            }

            var intent = new Intent(Intent.ActionSend);

            intent.PutExtra(Intent.ExtraEmail, email.To.Select(x => x.Address).ToArray());
            intent.PutExtra(Intent.ExtraCc, email.Cc.Select(x => x.Address).ToArray());
            intent.PutExtra(Intent.ExtraBcc, email.Bcc.Select(x => x.Address).ToArray());

            intent.PutExtra(Intent.ExtraTitle, email.Subject ?? string.Empty);

            if (email.IsHTML)
            {
                intent.PutExtra(Intent.ExtraText, Html.FromHtml(email.Body));
            }
            else
            {
                intent.PutExtra(Intent.ExtraText, email.Body ?? string.Empty);
            }

            intent.SetType("message/rfc822");

            StartActivity(intent);

            return Task.FromResult(true);
        }

        public virtual Task ComposeSMS(string recipient, string message = null)
        {
            if (!CanComposeSMS)
            {
                throw new FeatureNotAvailableException();
            }

            var uri = Uri.Parse(string.Format("sms:{0}", recipient));

            var intent = new Intent(Intent.ActionSendto, uri);
            intent.PutExtra("sms_body", message ?? string.Empty);

            StartActivity(intent);

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

            var uri = Uri.Parse(string.Format("tel:{0}", recipient));
            var intent = new Intent(Intent.ActionSendto, uri);
            StartActivity(intent);

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


            return Task.FromResult(true);
        }

        protected override void OnHandleIntent(Intent intent)
        {
        }
    }
}