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

        public LoginPageModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            Username = _settingsService.Username;
            Password = _settingsService.Password;
        }

        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    LoggedIn = true;
                    _settingsService.Username = Username;
                    _settingsService.Password = Password;
                    await CoreMethods.PushPageModel<CalculatorPageModel>();
                });
            }
        }
    }
}
