using System;

namespace Telephony
{
    public interface ITelephonyService
    {
        void ComposeEmail(string to, string subject, string body);

        void ComposeSms(string number, string message);

        void MakeVideoCall(string name, string number);

        void MakePhoneCall(string name, string number);
    }
}

