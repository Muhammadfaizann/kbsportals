﻿using System;
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
    public class FormattedEntry : NumericEntry
    {
        public void ValueChanged(object sender, TextChangedEventArgs args)
        {
            // override with custom behaviour
        }

        public IValueConverter Converter { get; set; }

        public FormattedEntry(IValueConverter converter) : base()
        {
            Converter = converter;
            Text = (string) Converter.Convert(Value, null, null, null);
            this.Focused += OnFocused;
            this.Unfocused += OnUnfocused;
        }

        protected override string FormatValue(decimal value)
        {
            return (string) Converter.Convert(value, typeof(string), null, null);
        }

        private void OnUnfocused(object sender, FocusEventArgs focusEventArgs)
        {
            Value = (decimal) Converter.ConvertBack(Text, null, null, null);
            Text = (string) Converter.Convert(Value, null, null, null);
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            Text = Value == 0.0m ? "" : Value.ToString();
        }
    }
}