using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
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
            var calculatorModel = new CalculatorModel()
            {
                Product = Product.Lease,
                Frequency = Frequency.Monthly,
                StartDate = DateTime.Today,
                NextDate = DateTime.Today.AddMonths(1)
            };
            CalculatorCarousel.ItemsSource = new List<Tuple<CalculationType, CalculatorModel>>
            {
                Tuple.Create(CalculationType.APRInstallment, calculatorModel),
                Tuple.Create(CalculationType.IRRInstallment, calculatorModel),
                Tuple.Create(CalculationType.Rate, calculatorModel)
            };
            CalculatorCarousel.Position = 0;
            UpdateTitle();
            CalculatorCarousel.PositionSelected += PositionSelected;
        }

        private void UpdateTitle()
        {
            Title = (CalculatorCarousel.ItemsSource[CalculatorCarousel.Position] as Tuple<CalculationType, CalculatorModel>).Item1.ToString();
        }

        private void PositionSelected(object sender, EventArgs eventArgs)
        {
            UpdateTitle();
        }
    }
}
