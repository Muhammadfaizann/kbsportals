using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Behaviours;
using KBS.Portals.Calculator.CustomViews;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.ValueConverters;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Views
{
    public partial class CalculatorView : ContentView
    {
        private PositiveNumberBehavior PositiveNumberBehavior;
        public CalculatorView()
        {
            InitializeComponent();
            PositiveNumberBehavior = new PositiveNumberBehavior();
            ProductPicker.SelectedIndexChanged += ProductPickerOnSelectedIndexChanged;
            UpFrontNo.TextChanged += UpFrontNoOnTextChanged;
            ProductPicker.ItemsSource = Enum.GetValues(typeof(Product));
            FrequencyPicker.ItemsSource = Enum.GetValues(typeof(Frequency));
        }

        private void UpFrontNoOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
            if (calculatorCarouselModel != null)
            {
                int value;
                int.TryParse(textChangedEventArgs.NewTextValue, out value);
                UpFrontValue.IsEnabled = calculatorCarouselModel.CalculationType == CalculationType.Rate && value > 0;
            }
        }

        private void ProductPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            Product product = (Product) ProductPicker.SelectedItem;
            if (product == Product.HirePurchase)
            {
                PurFee.Behaviors.Add(PositiveNumberBehavior);
                PositiveNumberBehavior.CheckEntryIsValid(PurFee, null);
            }
            else
            {
                PurFee.Behaviors.Remove(PositiveNumberBehavior);
                PositiveNumberBehavior.Validate(PurFee);
            }
        }

        protected override void OnBindingContextChanged()
        {
            var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
            if (calculatorCarouselModel != null)
            {
                var calcType = calculatorCarouselModel.CalculationType;
                APR.IsEnabled = calcType == CalculationType.APRInstallment;
                IRR.IsEnabled = calcType == CalculationType.IRRInstallment;
                Installment.IsEnabled = calcType == CalculationType.Rate;
            }
            base.OnBindingContextChanged();
        }
    }
}