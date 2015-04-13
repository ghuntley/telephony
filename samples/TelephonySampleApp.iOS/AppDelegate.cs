using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;
using ReactiveUI;
using TelephonySampleApp.Core;
using Xamarin.Forms;

using Foundation;

using UIKit;
using Splat;
using Telephony;

namespace TelephonySampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        AutoSuspendHelper suspendHelper;

        public AppDelegate()
        {
            RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();
            
            Locator.CurrentMutable.RegisterConstant(new TelephonyService(), typeof(ITelephonyService));
            
            UserError.RegisterHandler(ue =>
            {
                Debug.WriteLine(String.Format("Error: {0}", ue.ErrorMessage));
                Debug.WriteLine(String.Format("Exception: {0}", ue.InnerException));

                return Observable.Return(RecoveryOptionResult.CancelOperation);

            });
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
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