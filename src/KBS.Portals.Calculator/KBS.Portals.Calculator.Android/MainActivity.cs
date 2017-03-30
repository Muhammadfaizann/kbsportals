using Android.App;
using Android.Content.PM;
using Android.OS;
using FreshMvvm;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using KBS.Portals.Calculator.Droid.Services;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Droid
{
    [Activity(Theme = "@style/MainTheme", NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private readonly string APP_ID = "73871aab6c0c4b64be0eec934803394a";
        protected override void OnCreate(Bundle bundle)
        {
            FreshIOC.Container.Register<IQuitApplicationService, QuitApplicationService>();
            base.OnCreate(bundle);
            
            ActionBar.SetIcon(null);
            SetupHockeyApp();
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterManagers();
        }

        private void SetupHockeyApp()
        {
            CrashManager.Register(this, APP_ID);
            MetricsManager.Register(Application, APP_ID);
            CheckForUpdates();
        }

        private void CheckForUpdates()
        {
            // Remove this for store builds!
            UpdateManager.Register(this, APP_ID);
        }

        private void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }
    }
}