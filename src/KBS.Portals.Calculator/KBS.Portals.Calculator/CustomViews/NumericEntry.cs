using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.CustomViews
{
    public class NumericEntry : Entry
    {
        public NumericEntry()
        {
            Keyboard = Keyboard.Numeric;
            TextChanged += (sender, args) =>
            {
                decimal newVal;
                var couldParse = decimal.TryParse(args.NewTextValue, out newVal);
                Value = couldParse ? newVal : 0;
            };
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(decimal),
            typeof(NumericEntry), 0.0m, BindingMode.TwoWay);


        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}
