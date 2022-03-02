using System;
using System.Collections.Generic;
using System.Text;
using CarouselView.FormsPlugin.Abstractions;
using FreshMvvm;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.PageModels;
using KBS.Portals.Calculator.Services;
using KBS.Portals.Calculator.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBS.Portals.Calculator.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Android)
                ReinitialiseCarousel();
            ToolbarItems.Add(new ToolbarItem("Reset Calculator", "reset_calculator", () =>
            {
                var settingsService = FreshIOC.Container.Resolve<ISettingsService>();
                settingsService.PurFee = 0;
                settingsService.APR = 0;
                settingsService.DocFee = 0;
                settingsService.IRR = 0;
                settingsService.NoOfInstallments = 0;

                var calculatorModel = FreshIOC.Container.Resolve<CalculatorModel>();
                calculatorModel.Init();
            }));
        }

        private void ReinitialiseCarousel()
        {
            var binding = new Binding(nameof(CalculatorPageModel.PageModels));
            if (CalculatorCarousel != null) MainGrid.Children.Remove(CalculatorCarousel);
            CalculatorCarousel = new CarouselViewControl
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AnimateTransition = true,
                Orientation = Orientation.Horizontal,
                ShowIndicators = true,
                ItemTemplate = new DataTemplate(typeof(CalculatorView))
            };
            CalculatorCarousel.SetBinding(CarouselViewControl.ItemsSourceProperty, binding);
            CalculatorCarousel.Position = 0;
            CalculatorCarousel.PositionSelected += PositionSelected;
            MainGrid.Children.Add(CalculatorCarousel, 0, 0);
        }

        public CarouselViewControl CalculatorCarousel { get; set; }

        private void PositionSelected(object sender, EventArgs eventArgs)
        {
            var calculatorPageModel = (BindingContext as CalculatorPageModel);
            calculatorPageModel?.OnPositionSelected(sender, eventArgs);
        }

        private void CalculateButtonClicked(object sender, EventArgs eventArgs)
        {
            var pageModel = BindingContext as CalculatorPageModel;
            pageModel.Calculate.Execute(null);
            BuildSummaryGrid(pageModel.ResultDataSummary);
        }

        private void BuildSummaryGrid(IList<KeyValuePair<string, string>> results)
        {
            var resultsGrid = new Grid();
            resultsGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Star
            });
            resultsGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Star
            });
            int rowIndex = 0;
            foreach (var keyValuePair in results)
            {
                resultsGrid.RowDefinitions.Add(new RowDefinition{Height = GridLength.Star});
                resultsGrid.Children.Add(new Label{Text = keyValuePair.Key}, 0, rowIndex);
                resultsGrid.Children.Add(new Label{Text = keyValuePair.Value}, 1, rowIndex);
                rowIndex++;
            }
            ResultsView.Content = resultsGrid;
            SummaryPopup.IsVisible = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.iOS)
                ReinitialiseCarousel();
        }

        private void DismissSummary(object sender, EventArgs e)
        {
            SummaryPopup.IsVisible = false;
        }
        private void EmailSummary(object sender, EventArgs e)
        {
            try
            {
                var pageModel = BindingContext as CalculatorPageModel;
                StringBuilder emailContent = new StringBuilder();
                emailContent.Append("Calculation Results Summary");
                emailContent.Append("\n");
                foreach (var kvp in pageModel.ResultDataSummary)
                {
                    emailContent.Append(kvp.Key + ":" + kvp.Value);
                    emailContent.Append("\n");
                }
                Launcher.OpenAsync(new Uri("mailto:?subject=Calculation Summary&body=" + emailContent));
            }
            catch (Exception ex)
            {
                SummaryPopup.IsVisible = false;
                DisplayAlert("Email", "There was an error sending email, please try again","OK");
            }
        }
    }
}