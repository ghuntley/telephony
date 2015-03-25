using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;


using ReactiveUI;
using Splat;
using Telephony;
using System.Globalization;

namespace TelephonySampleApp.Core.ViewModels
{
    public interface IHomeViewModel : IRoutableViewModel
    {
        string Recipient { get; set; }

        ReactiveCommand<Unit> ComposeSMS { get; set; }

        ReactiveCommand<Unit> ComposeEmail { get; set; }

        ReactiveCommand<Unit> MakeVideoCall { get; set; }

        ReactiveCommand<Unit> MakePhoneCall { get; set; }
    }

    [DataContract]
    public class HomeViewModel : ReactiveObject, IHomeViewModel, IEnableLogger
    {
        [IgnoreDataMember]
        private readonly ITelephonyService TelephonyService;
        [IgnoreDataMember]
        private readonly string _recipient;

        public HomeViewModel(ITelephonyService telephonyService = null, IScreen hostScreen = null)
        {
            TelephonyService == telephonyService ?? Locator.Current.GetService<ITelephonyService>();

            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            var canComposeSMS = this.WhenAnyValue(vm => IsAValidPhoneNumber(vm.Recipient) && vm.TelephonyService.CanComposeSMS);
            ComposeSMS = ReactiveCommand.CreateAsyncTask(canComposeSMS, async _ =>
            {

                await TelephonyService.ComposeSms(Recipient); 
            });
            ComposeSMS.ThrownExceptions.Subscribe(ex => UserError.Throw("Does this device have the capability to send SMS?", ex));

            var canComposeEmail = this.WhenAnyValue(vm => IsAValidEmailAddress(vm.Recipient) && vm.TelephonyService.CanComposeEmail);
            ComposeEmail = ReactiveCommand.CreateAsyncTask(canComposeEmail, async _ =>
            {
                var email = new Email(receipients: Recipient);
                
                await TelephonyService.ComposeEmail(email); 
            });
            ComposeEmail.ThrownExceptions.Subscribe(ex => UserError.Throw("Recipient potentially is not a well formed email address.", ex));

            var canMakePhoneCall = this.WhenAnyValue(vm => IsAValidPhoneNumber(vm.Recipient) && vm.TelephonyService.CanMakePhoneCall);
            MakePhoneCall = ReactiveCommand.CreateAsyncTask(canMakePhoneCall, async _ =>
            {

                await TelephonyService.MakePhoneCall(Recipient); 
            });
            MakePhoneCall.ThrownExceptions.Subscribe(ex => UserError.Throw("Does this device have the capability to make phone calls?", ex));

            var canMakeVideoCall = this.WhenAnyValue(vm => (IsAValidPhoneNumber(vm.Recipient) || IsAValidEmailAddress(vm.Recipient)) && vm.TelephonyService.CanMakeVideoCall);
            MakeVideoCall = ReactiveCommand.CreateAsyncTask(canMakeVideoCall, async _ =>
            {

                await TelephonyService.MakeVideoCall(Recipient); 
            });
            MakeVideoCall.ThrownExceptions.Subscribe(ex => UserError.Throw("Does this device have the capability to make video calls?", ex));
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

        private static bool IsAValidEmailAddress(string s)
        {
            return s.Contains("@");
        }

        [DataMember]
        public string Recipient
        {
            get { return _recipient; }
            set { this.RaiseAndSetIfChanged(ref _recipient, value); }
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit> ComposeSMS
        {
            get;
            set;
        }

        [IgnoreDataMember]
        public ReactiveCommand<Unit> ComposeEmail
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

        [IgnoreDataMember]
        public ReactiveCommand<Unit> MakePhoneCall
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
        public string UrlPathSegment
        {
            get { return "Home"; }
        }

    }
}
