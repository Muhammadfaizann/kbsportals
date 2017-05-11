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
        }

        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(decimal),
            typeof(NumericEntry), 0.0m, BindingMode.TwoWay, propertyChanged: PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var formattedEntry = (NumericEntry)bindable;
            formattedEntry.Text = formattedEntry.FormatValue((decimal) newValue);
        }

        protected virtual string FormatValue(decimal value)
        {
            return value.ToString();
        }

        public decimal Value
        {
            get { return (decimal) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}