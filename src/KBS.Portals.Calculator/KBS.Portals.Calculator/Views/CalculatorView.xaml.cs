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
        private PositiveNumberBehavior PositiveNumberBehavior;
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
            PositiveNumberBehavior = new PositiveNumberBehavior();
            ProductPicker.SelectedIndexChanged += ProductPickerOnSelectedIndexChanged;
        }

        private void ProductPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            Product product = (Product) new ProductValueConverter().ConvertBack(ProductPicker.SelectedIndex, null, null, null);
            if (product == Product.HirePurchase)
            {
                PurFee.Behaviors.Add(PositiveNumberBehavior);
            }
            else
            {
                PurFee.Behaviors.Remove(PositiveNumberBehavior);
            }
        }

        protected override void OnBindingContextChanged()
        {
            var tuple = BindingContext as Tuple<CalculationType, CalculatorModel>;
            if (tuple != null)
            {
                var calcType = tuple.Item1;
                APR.IsEnabled = calcType == CalculationType.APRInstallment;
                IRR.IsEnabled = calcType == CalculationType.IRRInstallment;
                Installment.IsEnabled = calcType == CalculationType.Rate;
            }
            base.OnBindingContextChanged();
        }
    }
}