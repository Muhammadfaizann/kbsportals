using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FreshMvvm;
using KBS.Portals.Calculator.Enums;
using KBS.Portals.Calculator.Pages;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.PageModels
{
    public class LoginPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool RememberCredentials { get; set; }
        public bool LoggedIn { get; set; }
        private readonly ISettingsService _settingsService;
        private readonly IQuitApplicationService _quitApplicationService;
        private readonly IAuthenticationService _authenticationService;

        public LoginPageModel(ISettingsService settingsService, IQuitApplicationService quitApplicationService, IAuthenticationService authenticationService)
        {
            _settingsService = settingsService;
            _authenticationService = authenticationService;
            _quitApplicationService = quitApplicationService;
            Username = _settingsService.Username;
            Password = _settingsService.Password;
        }

        public void Quit()
        {
            _quitApplicationService.Quit();
        }

        public Command Login =>
            new Command(async () =>
            {
                IsBusy = true;
                bool loggedIn = await _authenticationService.LogIn(Username, Password);
                IsBusy = false;
                if (loggedIn)
                {
                    LoginSuccess();
                }
                else
                {
                    LoginFailure();
                }
            });
        

        private void LoginFailure()
        {
            Password = "";
            (CurrentPage as LoginPage)?.DisplayLoginFailed();
        }

        private void LoginSuccess()
        {
            LoggedIn = true;
            _settingsService.Username = Username;
            _settingsService.Password = Password;
            var mainContainer = new FreshMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
            mainContainer.AddPage<CalculatorPageModel>("Calculate");
            mainContainer.AddPage<LogoutPageModel>("Log out");
            mainContainer.Init("Menu");
            Application.Current.MainPage = mainContainer;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}