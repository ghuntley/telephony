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

namespace Telephony
{
    public class TelephonyService : ITelephonyService
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
                return true;
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
            try
            {

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
                //this.StartActivity(intent);
            //            Device.StartActivity(intent);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task ComposeSMS(string recipient, string message = null)
        {
            try
            {
                var uri = Android.Net.Uri.Parse(String.Format("smsto:{0}", recipient));
                var intent = new Intent(Intent.ActionSendto, uri);
                intent.PutExtra("sms_body", message ?? string.Empty);
                //            StartActivity(smsIntent);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task MakePhoneCall(string recipient, string displayName = null)
        {
            try
            {
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task MakeVideoCall(string recipient, string displayName = null)
        {
            try
            {
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}