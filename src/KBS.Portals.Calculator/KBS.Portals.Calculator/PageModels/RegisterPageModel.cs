using System.ComponentModel;
using System.Runtime.CompilerServices;
using FreshMvvm;
using KBS.Portals.Calculator.Behaviours;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.PageModels
{
    class RegisterPageModel : FreshBasePageModel
    {
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

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public Command Register => new Command(() =>
            {
                var emailValidator = new EmailValidatorBehaviour();
                var email = CurrentPage.FindByName<Entry>("EmailEntry");

                if (!emailValidator.IsValid(email) || string.IsNullOrWhiteSpace(Name))
                {
                    CurrentPage.DisplayAlert("Data required",
                        "You must type a Name and a valid Email Address",
                        "Close");
                }
                else
                {
                    if (RegisterApi())
                    {
                        CurrentPage.DisplayAlert("Register sent",
                            "Thank you for registering your interest, we will be in touch soon",
                            "Close");
                    }
                    else
                    {
                        CurrentPage.DisplayAlert("Error",
                            "Unfortunately something has gone wrong, please try again later",
                            "Close");
                    }
                }
            }
        );

        private bool RegisterApi()
        {
            return false;
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
