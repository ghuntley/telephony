using System;
using System.Threading.Tasks;


namespace Telephony
{
    public interface ITelephonyService
    {
        bool ComposeEmailFeatureEnabled { get; }

        Task ComposeEmail(Email email);

        bool ComposeSMSFeatureEnabled { get ; }

        Task ComposeSMS(string recipient, string message = null);

        bool MakeVideoCallFeatureEnabled { get; }

        Task MakeVideoCall(string recipient, string displayName = null);

        bool MakePhoneCallFeatureEnabled { get; }

        Task MakePhoneCall(string recipient, string displayName = null);
    }
}

