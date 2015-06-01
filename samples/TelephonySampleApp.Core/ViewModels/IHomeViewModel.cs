using System.Reactive;
using ReactiveUI;

namespace TelephonySampleApp.Core.ViewModels
{
    public interface IHomeViewModel : IRoutableViewModel
    {
        ReactiveCommand<Unit> ComposeEmail { get; set; }

        ReactiveCommand<Unit> ComposeSMS { get; set; }

        ReactiveCommand<Unit> MakePhoneCall { get; set; }

        ReactiveCommand<Unit> MakeVideoCall { get; set; }

        string Recipient { get; set; }
    }
}