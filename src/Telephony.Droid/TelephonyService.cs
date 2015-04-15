using System;
using System.Linq;
using System.Threading.Tasks;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Telephony;


namespace Telephony
{
    public class TelephonyService : IntentService, ITelephonyService
    {
        public TelephonyService()
        {
        }

        public bool CanComposeEmail
        {
            get
            {
                return true;
            }
        }

        public bool CanComposeSMS
        {
            get
            {
                return true;
            }
        }

        public bool CanMakePhoneCall
        {
            get
            {
                var telephonyManager = this.GetSystemService(Context.TelephonyService) as TelephonyManager;

                return telephonyManager.PhoneType != PhoneType.None;
            }
        }

        public bool CanMakeVideoCall
        {
            get
            {
                return true;
            }
        }

        public Task ComposeEmail(Email email)
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
                intent.PutExtra(Intent.ExtraText, Android.Text.Html.FromHtml(email.Body));
            }
            else
            {
                intent.PutExtra(Intent.ExtraText, email.Body ?? string.Empty);
            }

            intent.SetType("message/rfc822");

            return Task.FromResult(true);
//                this.StartActivity(intent);
//                Device.StartActivity(intent);
        }

        public Task ComposeSMS(string recipient, string message = null)
        {
            if (!CanComposeSMS)
            {
                throw new FeatureNotAvailableException();
            }
            
            var uri = Android.Net.Uri.Parse(String.Format("smsto:{0}", recipient));
            var intent = new Intent(Intent.ActionSendto, uri);
            intent.PutExtra("sms_body", message ?? string.Empty);
            //            StartActivity(smsIntent);

            return Task.FromResult(true);
        }

        public Task MakePhoneCall(string recipient, string displayName = null)
        {
            if (String.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentNullException("recipient", "Supplied argument 'recipient' is null, whitespace or empty.");
            }

            if (!CanMakePhoneCall)
            {
                throw new FeatureNotAvailableException();
            }
 
            var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", recipient));
            var intent = new Intent(Intent.ActionSendto, uri);
            //            StartActivity(smsIntent);
            
            return Task.FromResult(true);
        }

        public Task MakeVideoCall(string recipient, string displayName = null)
        {
            if (String.IsNullOrWhiteSpace(recipient))
            {
                throw new ArgumentNullException("recipient", "Supplied argument 'recipient' is null, whitespace or empty.");
            }
            
            if (!CanMakeVideoCall)
            {
                throw new FeatureNotAvailableException();
            }
            
            
            return Task.FromResult(true);
        }
    }
}