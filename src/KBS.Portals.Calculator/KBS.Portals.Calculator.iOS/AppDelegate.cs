using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using HockeyApp.iOS;
using UIKit;

namespace KBS.Portals.Calculator.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private readonly string APP_ID = "51ca719b4b6341808160babab9fa819c";
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();
            LoadApplication(new App());
            SetupHockeyApp();

            return base.FinishedLaunching(app, options);
        }

        private void SetupHockeyApp()
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(APP_ID);
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds
        }
    }
}
