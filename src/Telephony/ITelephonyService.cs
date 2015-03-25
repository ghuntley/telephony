using System;
using System.Threading.Tasks;


namespace Telephony
{
    public interface ITelephonyService
    {
        bool ComposeEmailFeatureEnabled { get; }

        Task ComposeEmail(Email email);

        bool ComposeSMSFeatureEnabled { get ; }

        Task ComposeSMS(string recipient, string message);

        bool MakeVideoCallFeatureEnabled { get; }

        Task MakeVideoCall(string recipient, string displayName);

        bool MakePhoneCallFeatureEnabled { get; }

        Task MakePhoneCall(string recipient, string displayName);
    }
}

