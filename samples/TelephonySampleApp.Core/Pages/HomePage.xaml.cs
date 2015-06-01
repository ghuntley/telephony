using TelephonySampleApp.Core.ViewModels;

namespace TelephonySampleApp.Core.Pages
{
    public class HomePage : ContentPage, IViewFor<HomeViewModel>
    {
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<HomePage, HomeViewModel>(x => x.ViewModel, default(HomeViewModel),
                BindingMode.OneWay);

        public HomePage()
        {
            InitializeComponent();

            this.Bind(ViewModel, vm => vm.Recipient, v => v.Recipient.Text);

            this.BindCommand(ViewModel, vm => vm.ComposeEmail, v => v.ComposeEmail);
            this.BindCommand(ViewModel, vm => vm.ComposeSMS, v => v.ComposeSMS);
            this.BindCommand(ViewModel, vm => vm.MakePhoneCall, v => v.MakePhoneCall);
            this.BindCommand(ViewModel, vm => vm.MakeVideoCall, v => v.MakeVideoCall);
        }

        public HomeViewModel ViewModel
        {
            get { return (HomeViewModel) GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HomeViewModel) value; }
        }
    }
}