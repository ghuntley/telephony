using Foundation;
using ReactiveUI;
using Splat;
using Telephony;
using TelephonySampleApp.Core;
using Toasts.Forms.Plugin.iOS;
using UIKit;
using Xamarin.Forms;

namespace TelephonySampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private AutoSuspendHelper suspendHelper;
        private UIWindow window;

        public AppDelegate()
        {
            RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();

            Locator.CurrentMutable.RegisterConstant(new TelephonyService(), typeof (ITelephonyService));
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            ToastNotificatorImplementation.Init();

            RxApp.SuspensionHost.SetupDefaultSuspendResume();

            suspendHelper = new AutoSuspendHelper(this);
            suspendHelper.FinishedLaunching(app, options);

            window = new UIWindow(UIScreen.MainScreen.Bounds);
            var bootstrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();

            window.RootViewController = bootstrapper.CreateMainPage().CreateViewController();
            window.MakeKeyAndVisible();

            return true;
        }

        public override void DidEnterBackground(UIApplication application)
        {
            suspendHelper.DidEnterBackground(application);
        }

        public override void OnActivated(UIApplication application)
        {
            suspendHelper.OnActivated(application);
        }
    }
}