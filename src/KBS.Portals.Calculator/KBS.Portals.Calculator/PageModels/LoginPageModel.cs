using FreshMvvm;
using KBS.Portals.Calculator.Enums;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberCredentials { get; set; }
        public bool LoggedIn { get; set; }
        private ISettingsService _settingsService;
        private readonly IQuitApplicationService _quitApplicationService;

        public LoginPageModel(ISettingsService settingsService, IQuitApplicationService quitApplicationService)
        {
            _settingsService = settingsService;
            _quitApplicationService = quitApplicationService;
            Username = _settingsService.Username;
            Password = _settingsService.Password;
        }

        public void Quit()
        {
            _quitApplicationService.Quit();
        }

        public Command Login
        {
            get
            {
                return new Command(() =>
                {
                    LoggedIn = true;
                    _settingsService.Username = Username;
                    _settingsService.Password = Password;
                    var mainContainer = new FreshMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
                    mainContainer.AddPage<CalculatorPageModel>("Calculate");
                    mainContainer.AddPage<LogoutPageModel>("Log out");
                    mainContainer.Init("Menu");
                    Application.Current.MainPage = mainContainer;
                });
            }
        }
    }
}
