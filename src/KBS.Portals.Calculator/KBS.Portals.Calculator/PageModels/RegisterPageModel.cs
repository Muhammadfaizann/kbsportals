﻿using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using KBS.Portals.Calculator.Behaviours;
using Newtonsoft.Json;
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

        public Command Register => new Command(async () =>
            {
                var emailValidator = new EmailValidatorBehaviour();
                var email = CurrentPage.FindByName<Entry>("EmailEntry");

                if (!emailValidator.IsValid(email) || string.IsNullOrWhiteSpace(Name))
                {
                    await CurrentPage.DisplayAlert("Data required",
                        "You must type a Name and a valid Email Address",
                        "Close");
                }
                else
                {
                    if (await RegisterApi())
                    {
                        await CurrentPage.DisplayAlert("Register sent",
                            "Thank you for registering your interest, we will be in touch soon",
                            "Close");
                    }
                    else
                    {
                        await CurrentPage.DisplayAlert("Error",
                            "Unfortunately something has gone wrong, please try again later",
                            "Close");
                    }
                }
            }
        );

        private async Task<bool> RegisterApi()
        {
            using (HttpClient client = new HttpClient())
            {
#if DEBUG
                var uri = new Uri("https://dev.local/api/register");
#else
                var uri = new Uri("https://kbs.ie/api/register");
#endif

                try
                {
                    var data = new {Name, Email};
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }

                return false;
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
