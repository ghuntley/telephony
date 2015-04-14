using System;
using System.Threading.Tasks;


namespace Telephony
{
    public interface ITelephonyService
    {
        bool CanComposeEmail { get; }

        Task ComposeEmail(Email email);

        bool CanComposeSMS { get ; }

        Task ComposeSMS(string recipient, string message = null);

        bool CanMakeVideoCall { get; }

        Task MakeVideoCall(string recipient, string displayName = null);

        bool CanMakePhoneCall { get; }

        Task MakePhoneCall(string recipient, string displayName = null);
    }
}

