using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using ReactiveUI;
using TelephonySampleApp.Core;
using Toasts.Forms.Plugin.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TelephonySampleApp.Droid
{
    [Activity(Label = "Telephony", Icon = "@drawable/icon_white", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        public MainActivity()
        {
            Console.WriteLine("Start");
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            ToastNotificatorImplementation.Init();


            var mainPage = RxApp.SuspensionHost.GetAppState<AppBootstrapper>().CreateMainPage();
            SetPage(mainPage);
        }
    }
}