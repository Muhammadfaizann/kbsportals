using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.ValueConverters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KBS.Portals.Calculator.CustomViews
{
    class FormattedEntry : Entry
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(decimal),
            typeof(FormattedEntry), 0.0m, BindingMode.TwoWay);


        public decimal Value
        {
            get { return (decimal) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public void ValueChanged(object sender, TextChangedEventArgs args)
        {
            // override with custom behaviour
        }

        public IValueConverter Converter { get; set; }

        public FormattedEntry(IValueConverter converter)
        {
            Converter = converter;
            Text = (string) Converter.Convert(Value, null, null, null);
            this.Focused += OnFocused;
            this.Unfocused += OnUnfocused;
        }
        
        private void OnUnfocused(object sender, FocusEventArgs focusEventArgs)
        {
            Value = (decimal) Converter.ConvertBack(Text, null, null, null);
            Text = (string) Converter.Convert(Value, null, null, null);
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            Text = Value.ToString();
        }
    }
}