using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

using ReactiveUI;

using Splat;

using Telephony;

namespace TelephonySampleApp.Core.ViewModels
{
    public interface IHomeViewModel : IRoutableViewModel
    {
        ReactiveCommand<Unit> ComposeEmail
        {
            get;
            set;
        }

        ReactiveCommand<Unit> ComposeSMS
        {
            get;
            set;
        }

        ReactiveCommand<Unit> MakePhoneCall
        {
            get;
            set;
        }

        ReactiveCommand<Unit> MakeVideoCall
        {
            get;
            set;
        }

        string Recipient
        {
            get;
            set;
        }
    }

    [DataContract]
    public class HomeViewModel : ReactiveObject, IHomeViewModel, IEnableLogger
    {
        [IgnoreDataMember]
        private readonly ITelephonyService TelephonyService;

        [IgnoreDataMember]
        private string _recipient;

        public HomeViewModel(ITelephonyService telephonyService = null, IScreen hostScreen = null)
        {
            TelephonyService = telephonyService ?? Locator.Current.GetService<ITelephonyService>();

            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            var canComposeSMS = this.WhenAny(x => x.Recipient, x => !String.IsNullOrWhiteSpace(x.Value));
            ComposeSMS = ReactiveCommand.CreateAsyncTask(canComposeSMS, async _ =>
            {
                await TelephonyService.ComposeSMS(Recipient);
            });
            ComposeSMS.ThrownExceptions.Subscribe(ex => UserError.Throw("Does this device have the capability to send SMS?", ex));

            var canComposeEmail = this.WhenAny(x => x.Recipient, x => !String.IsNullOrWhiteSpace(x.Value));
            ComposeEmail = ReactiveCommand.CreateAsyncTask(canComposeEmail, async _ =>
            {
                var email = new Email(receipients: Recipient);

                await TelephonyService.ComposeEmail(email);
            });
            ComposeEmail.ThrownExceptions.Subscribe(ex => UserError.Throw("The recipient is potentially not a well formed email address.", ex));

            var canMakePhoneCall = this.WhenAny(x => x.Recipient, x => !String.IsNullOrWhiteSpace(x.Value));
            MakePhoneCall = ReactiveCommand.CreateAsyncTask(canMakePhoneCall, async _ =>
            {
                await TelephonyService.MakePhoneCall(Recipient);
            });
            MakePhoneCall.ThrownExceptions.Subscribe(ex => UserError.Throw("Does this device have the capability to make phone calls?", ex));

            var canMakeVideoCall = this.WhenAny(x => x.Recipient, x => !String.IsNullOrWhiteSpace(x.Value));
            MakeVideoCall = ReactiveCommand.CreateAsyncTask(canMakeVideoCall, async _ =>
            {

                await TelephonyService.MakeVideoCall(Recipient);
            });
            MakeVideoCall.ThrownExceptions.Subscribe(ex => UserError.Throw("Does this device have the capability to make video calls?", ex));
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit> ComposeEmail
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit> ComposeSMS
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public IScreen HostScreen
        {
            get;
            protected set;
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit> MakePhoneCall
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit> MakeVideoCall
        {
            get;
            set;
        }

        [DataMember]
        public string Recipient
        {
            get { return _recipient; }
            set { this.RaiseAndSetIfChanged(ref _recipient, value); }
        }

        [IgnoreDataMember]
        public string UrlPathSegment
        {
            get { return "Telephony"; }
        }


        private static bool IsAValidPhoneNumber(string s)
        {
            int result;
            var phoneNumber = s.Replace(" ", "")
                .Replace("-", "")
                .Replace("+", "")
                .Replace("(", "")
                .Replace(")", "");

            return int.TryParse(phoneNumber, out result);
        }
    }
}