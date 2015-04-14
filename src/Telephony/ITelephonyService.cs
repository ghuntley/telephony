using System;
using System.Threading.Tasks;


namespace Telephony
{
    public interface ITelephonyService
    {
        bool ComposeEmailFeatureAvailable { get; }

        Task ComposeEmail(Email email);

        bool ComposeSMSFeatureAvailable { get ; }

        Task ComposeSMS(string recipient, string message = null);

        bool MakeVideoCallFeatureAvailable { get; }

        Task MakeVideoCall(string recipient, string displayName = null);

        bool MakePhoneCallFeatureAvailable { get; }

        Task MakePhoneCall(string recipient, string displayName = null);
    }
}

