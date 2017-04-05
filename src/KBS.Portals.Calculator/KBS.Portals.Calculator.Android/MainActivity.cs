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
        public static readonly string AppId = "73871aab6c0c4b64be0eec934803394a";
        public static readonly string FeedbackEvent = "feedback";
        protected override void OnCreate(Bundle bundle)
        {
            FreshIOC.Container.Register<IApplicationService, ApplicationService>();
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
            CrashManager.Register(this, AppId);
            MetricsManager.Register(Application, AppId);
            CheckForUpdates();
            HockeyApp.MetricsManager.TrackEvent(FeedbackEvent);
        }

        private void CheckForUpdates()
        {
            // Remove this for store builds!
            UpdateManager.Register(this, AppId);
        }

        private void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }
    }
}