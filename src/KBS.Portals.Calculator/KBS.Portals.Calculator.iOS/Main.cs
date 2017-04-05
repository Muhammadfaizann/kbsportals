using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using FreshMvvm;
using KBS.Portals.Calculator.iOS.Services;
using KBS.Portals.Calculator.Services;
using UIKit;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            FreshIOC.Container.Register<IApplicationService, ApplicationService>();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
