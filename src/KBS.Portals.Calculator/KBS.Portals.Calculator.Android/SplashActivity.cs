using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarouselView.FormsPlugin.Android;

namespace KBS.Portals.Calculator.Droid
{
    [Activity(Label = "KBS Calculator", NoHistory = true, Icon = "@drawable/launcher_icon", Theme = "@style/Theme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            CarouselViewRenderer.Init();

            StartActivity(new Intent(this, typeof(MainActivity)));
        }
    }
}