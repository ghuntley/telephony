using System;
using Android.App;
using ReactiveUI;
using Android.Runtime;
using TelephonySampleApp.Core;
using Splat;
using Telephony;

namespace TelephonySampleApp.Droid
{

    [Application]
    public class AndroidApplication : Application
    {
        AutoSuspendHelper _autoSuspendHelper;

        public AndroidApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            _autoSuspendHelper = new AutoSuspendHelper(this);

            // CoolStuff: The job of AutoSuspendHelper is, that it will 
            // automatically save and reload exactly *one* object of your 
            // choice when the app is suspended. If the object can't be
            // reloaded (i.e. if the app is starting for the first time),
            // we're telling ReactiveUI here how to create a new one from
            // scratch.
            RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();

            RxApp.SuspensionHost.SetupDefaultSuspendResume();
            
            Locator.CurrentMutable.RegisterConstant(new TelephonyService(), typeof(ITelephonyService));
        }
    }
}

