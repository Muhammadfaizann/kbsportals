using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;
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
    }
}
