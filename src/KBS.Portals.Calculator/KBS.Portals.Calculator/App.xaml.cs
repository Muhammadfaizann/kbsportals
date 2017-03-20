using FreshMvvm;
using KBS.Portals.Calculator.Enums;
using KBS.Portals.Calculator.PageModels;
using Xamarin.Forms;

namespace KBS.Portals.Calculator
{
    public partial class App : Application
    {
        public App()
        {
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var navContainer = new FreshNavigationContainer(page, NavigationContainerNames.AuthenticationContainer);
            MainPage = navContainer;
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
