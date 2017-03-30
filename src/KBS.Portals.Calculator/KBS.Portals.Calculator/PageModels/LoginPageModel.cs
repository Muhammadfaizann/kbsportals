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

        private bool _rememberCredentials;

        public bool RememberCredentials
        {
            get { return _rememberCredentials; }
            set
            {
                _rememberCredentials = value;
                _settingsService.RememberMe = value;
                OnPropertyChanged();
            }
        }
        
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
            RememberCredentials = _settingsService.RememberMe;
        }

        public void Quit()
        {
            _quitApplicationService.Quit();
        }

        public Command Login =>
            new Command(async () =>
            {
                IsBusy = true;
                LoggedIn = await _authenticationService.LogIn(Username, Password);
                IsBusy = false;
                if (LoggedIn)
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
            LoggedIn = false;
            (CurrentPage as LoginPage)?.DisplayLoginFailed();
        }

        private void LoginSuccess()
        {
            LoggedIn = true;
            if (RememberCredentials)
            {
                _settingsService.Username = Username;
                _settingsService.Password = Password;
            }
            else
            {
                _settingsService.Username = "";
                _settingsService.Password = "";
            }
            var mainContainer = new FreshMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
            mainContainer.AddPage<CalculatorPageModel>("Calculate");
            mainContainer.AddPage<LogoutPageModel>("Log out");
            mainContainer.Init("Menu");
            Application.Current.MainPage = mainContainer;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}