using FreshMvvm;
using KBS.Portals.Calculator.Config;
using KBS.Portals.Calculator.Enums;
using KBS.Portals.Calculator.PageModels;
using KBS.Portals.Calculator.Pages;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetupIOC();
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            MainPage = page;
        }

        private void SetupIOC()
        {
            FreshIOC.Container.Register<ISettingsService, SettingsService>();
            FreshIOC.Container.Register<IAuthenticationService, AuthenticationService>();
            FreshIOC.Container.Register<IFeedbackService, FeedbackService>();
            AutoMapperConfig.Init();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
