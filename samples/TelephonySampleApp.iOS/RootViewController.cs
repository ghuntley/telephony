using System;

using CoreAnimation;

using Foundation;

using ObjCRuntime;

using OpenGLES;

using OpenTK;
using OpenTK.Graphics.ES20;
using OpenTK.Platform.iPhoneOS;

using UIKit;

namespace TelephonySampleApp.iOS
{
    [Register("RootViewController")]
    public partial class RootViewController : UIViewController
    {
        public RootViewController(IntPtr handle)
            : base(handle)
        {
        }

        new EAGLView View
        {
            get { return (EAGLView)base.View; }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillResignActiveNotification, a =>
            {
                if (IsViewLoaded && View.Window != null)
                    View.StopAnimating();
            }, this);
            NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, a =>
            {
                if (IsViewLoaded && View.Window != null)
                    View.StartAnimating();
            }, this);
            NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillTerminateNotification, a =>
            {
                if (IsViewLoaded && View.Window != null)
                    View.StopAnimating();
            }, this);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            View.StartAnimating();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            View.StopAnimating();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            NSNotificationCenter.DefaultCenter.RemoveObserver(this);
        }
    }
}