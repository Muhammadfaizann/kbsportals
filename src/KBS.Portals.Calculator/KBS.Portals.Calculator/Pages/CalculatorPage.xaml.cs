using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CarouselView.FormsPlugin.Abstractions;
using FreshMvvm;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.PageModels;
using KBS.Portals.Calculator.Services;
using KBS.Portals.Calculator.Views;
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
            if (Device.OS == TargetPlatform.Android)
                ReinitialiseCarousel();
            ToolbarItems.Add(new ToolbarItem("Reset Calculator", "reset_calculator", () =>
            {
                var settingsService = FreshIOC.Container.Resolve<ISettingsService>();
                settingsService.PurFee = 0;
                settingsService.APR = 0;
                settingsService.DocFee = 0;
                settingsService.IRR = 0;
                settingsService.Term = 0;

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
            SetSummaryVisibility(true);
        }

        private void SetSummaryVisibility(bool isVisible)
        {
            SummaryPopup.IsVisible = isVisible;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.OS == TargetPlatform.iOS)
                ReinitialiseCarousel();
        }

        private void DismissSummary(object sender, EventArgs e)
        {
            SetSummaryVisibility(false);
        }
    }
}
