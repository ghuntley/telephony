using System;

namespace Telephony
{
    public interface ITelephonyService
    {
        void ComposeEmail(Email email);

        void ComposeSms(string recipient, string message);

        void MakeVideoCall(string recipient, string displayName);

        void MakePhoneCall(string recipient, string displayName);
    }
}

