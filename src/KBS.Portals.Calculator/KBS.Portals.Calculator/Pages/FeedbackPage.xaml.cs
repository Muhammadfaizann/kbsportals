using System;
using System.Collections.Generic;
using AutoMapper;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.PageModels;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBS.Portals.Calculator.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        public FeedbackPage()
        {
            InitializeComponent();
            SubmitButton.Clicked += SubmitButtonOnClicked;
        }

        private async void SubmitButtonOnClicked(object sender, EventArgs eventArgs)
        {
            await (BindingContext as FeedbackPageModel)?.SubmitFeedback(UserInput.Text);
        }
    }
}
