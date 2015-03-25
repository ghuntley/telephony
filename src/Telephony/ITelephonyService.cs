using System;

namespace Telephony
{
    public interface ITelephonyService
    {
        bool CanComposeEmail { get; }

        void ComposeEmail(Email email);

        bool CanComposeSMS { get ; }

        void ComposeSms(string recipient, string message);

        bool CanMakeVideoCall { get; }

        void MakeVideoCall(string recipient, string displayName);

        bool CanMakePhoneCall { get; }

        void MakePhoneCall(string recipient, string displayName);
    }
}

