using Android.App;
using Android.Content.PM;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;

namespace KBS.Portals.Calculator.Droid
{
    [Activity(Theme = "@android:style/Theme.Material.Light", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private readonly string APP_ID = "73871aab6c0c4b64be0eec934803394a";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetupHockeyApp();
            global::Xamarin.Forms.Forms.Init(this, bundle);
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