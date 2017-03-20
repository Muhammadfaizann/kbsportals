using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Behaviours;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.ValueConverters;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Views
{
    public partial class CalculatorView : ContentView
    {

        public CalculatorView()
        {
            InitializeComponent();
            var products = Enum.GetNames(typeof(Product));
            foreach (var product in products)
            {
                ProductPicker.Items.Add(product);
            }
            var frequencies = Enum.GetNames(typeof(Frequency));
            foreach (var frequency in frequencies)
            {
                FrequencyPicker.Items.Add(frequency);
            }
        }

        protected override void OnBindingContextChanged()
        {
            var tuple = BindingContext as Tuple<CalculationType, CalculatorModel>;
            if (tuple != null)
            {
                var calcType = tuple.Item1;
                IRR.IsEnabled = calcType == CalculationType.IRRInstallment;
                Installment.IsEnabled = calcType == CalculationType.Rate;   
            }
            base.OnBindingContextChanged();
        }
    }
}