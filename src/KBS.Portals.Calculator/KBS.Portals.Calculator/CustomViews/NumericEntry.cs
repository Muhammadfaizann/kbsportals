using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                if (string.IsNullOrEmpty(args.NewTextValue))
                {
                    Value = 0.00m;
                }
                else
                {
                    string validDecimal = Regex.Replace(args.NewTextValue, "[,€%]", "");
                    Value = decimal.Parse(validDecimal);
                }
            };
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(decimal),
            typeof(NumericEntry), 0.0m, BindingMode.TwoWay);


        public decimal Value
        {
            get { return (decimal) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}