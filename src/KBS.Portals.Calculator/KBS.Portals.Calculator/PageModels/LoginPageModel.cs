using System.ComponentModel;
using System.Runtime.CompilerServices;
using FreshMvvm;
using KBS.Portals.Calculator.Enums;
using KBS.Portals.Calculator.Pages;
using KBS.Portals.Calculator.Services;
using KBS.Portals.Calculator.Views;
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
        
        private readonly ISettingsService _settingsService;
        private readonly IApplicationService _applicationService;
        private readonly IAuthenticationService _authenticationService;

        public LoginPageModel(ISettingsService settingsService, IApplicationService applicationService,
            IAuthenticationService authenticationService)
        {
            _settingsService = settingsService;
            _authenticationService = authenticationService;
            _applicationService = applicationService;
            RememberCredentials = _settingsService.RememberMe;
            if (RememberCredentials)
            {
                Username = _settingsService.Username;
                if (!string.IsNullOrEmpty(_settingsService.AccessToken)) TryTokenLogin();
            }
        }

        public void Quit()
        {
            _applicationService.Quit();
        }

        private async void TryTokenLogin()
        {
            IsBusy = true;
            bool tokenIsValid = await _authenticationService.ValidateToken(_settingsService.AccessToken);
            IsBusy = false;
            if (tokenIsValid)
            {
                LoginSuccess();
            }
            else
            {
                LoginFailure("The persisted access token has expired. Please re-enter your credentials.");
            }
        }

        public Command Login =>
            new Command(async () =>
            {
                IsBusy = true;
                string accessToken = await _authenticationService.LogIn(Username, Password);
                IsBusy = false;
                if (!string.IsNullOrEmpty(accessToken))
                {
                    _settingsService.AccessToken = RememberCredentials ? accessToken : null;
                    LoginSuccess();
                }
                else
                {
                    LoginFailure("Your username or password wasn't accepted by the server.");
                }
            });


        private void LoginFailure(string errorMsg)
        {
            (CurrentPage as LoginPage)?.DisplayLoginFailed(errorMsg);
        }

        private void LoginSuccess()
        {
            _settingsService.Username = Username;
            var mainContainer = new NavContainer(NavigationContainerNames.MainContainer);
            Application.Current.MainPage = mainContainer;
        }

        public Command Register => new Command(
            () =>
            {
                Application.Current.MainPage.Navigation.PushModalAsync(FreshPageModelResolver.ResolvePageModel<RegisterPageModel>());
            }
        );

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}