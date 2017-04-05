using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.PageModels;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.OS == TargetPlatform.iOS)
                ReinitialiseCarousel();
        }
    }
}
