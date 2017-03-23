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
            CalculatorCarousel.Position = 0;
            CalculatorCarousel.PositionSelected += PositionSelected;
        }

        private void PositionSelected(object sender, EventArgs eventArgs)
        {
            var calculatorPageModel = (BindingContext as CalculatorPageModel);
            calculatorPageModel?.OnPositionSelected(sender, eventArgs);
        }
    }
}
