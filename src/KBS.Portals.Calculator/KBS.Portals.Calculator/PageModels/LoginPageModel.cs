using FreshMvvm;
using KBS.Portals.Calculator.Enums;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberCredentials { get; set; }
        public bool LoggedIn { get; set; }

        public LoginPageModel()
        {
        }

        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    LoggedIn = true;
                    //CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.MainContainer);
                    await CoreMethods.PushPageModel<CalculatorPageModel>();
                });
            }
        }
    }
}
