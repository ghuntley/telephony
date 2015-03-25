using System;

using ReactiveUI;
using ReactiveUI.XamForms;

using Splat;

using TelephonySampleApp.Core.Pages;
using TelephonySampleApp.Core.ViewModels;

using Xamarin.Forms;

namespace TelephonySampleApp.Core
{
    public class AppBootstrapper : ReactiveObject, IScreen, IEnableLogger
    {
        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.Register(() => new HomePage(), typeof(IViewFor<HomeViewModel>));

            Router.Navigate.Execute(new HomeViewModel());
        }

        public RoutingState Router
        {
            get;
            protected set;
        }

        public Page CreateMainPage()
        {
            // NB: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrapper as an IScreen.
            return new RoutedViewHost();
        }
    }
}