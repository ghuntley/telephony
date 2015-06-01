using System.Threading.Tasks;

namespace Telephony
{
    public interface ITelephonyService
    {
        bool CanComposeEmail { get; }
        bool CanComposeSMS { get; }
        bool CanMakePhoneCall { get; }
        bool CanMakeVideoCall { get; }
        Task ComposeEmail(IEmailMessage emailMessage);
        Task ComposeSMS(string recipient, string message = null);
        Task MakePhoneCall(string recipient, string displayName = null);
        Task MakeVideoCall(string recipient, string displayName = null);
    }
}