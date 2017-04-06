using System.Linq;
using KBS.Portals.Calculator.PageModels;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBS.Portals.Calculator.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            var loginPageModel = BindingContext as LoginPageModel;
            loginPageModel.Quit();
            return base.OnBackButtonPressed();
        }

        public void DisplayLoginFailed(string errorMessage)
        {
            DisplayAlert("Log In Failed", errorMessage, "Okay");
        }
    }
}
