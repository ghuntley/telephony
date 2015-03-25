using System;
using System.Collections.Generic;
using TelephonySampleApp.Core.ViewModels;

using ReactiveUI;

using Xamarin.Forms;

namespace TelephonySampleApp.Core.Pages
{
    public partial class HomePage : ContentPage, IViewFor<HomeViewModel>
    {
        public static readonly BindableProperty ViewModelProperty = 
            BindableProperty.Create<HomePage, HomeViewModel>(x => x.ViewModel, default(HomeViewModel), BindingMode.OneWay);

        public HomePage()
        {
            InitializeComponent();

            //            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            //            this.Bind(ViewModel, vm => vm.Password, v => v.Password.Text);
            //
            //            this.BindCommand(ViewModel, vm => vm.Login, v => v.Login);
        }

        public HomeViewModel ViewModel
        {
            get { return (HomeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HomeViewModel)value; }
        }
    }
}