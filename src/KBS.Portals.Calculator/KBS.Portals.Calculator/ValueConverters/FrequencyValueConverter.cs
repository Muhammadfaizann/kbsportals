using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.ValueConverters
{
    class FrequencyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int) Array.IndexOf(Enum.GetValues(value.GetType()), value);
                // Enum values are reassigned in Frequency.cs so we have to match indexes
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Frequency) (Enum.GetValues(typeof(Frequency))).GetValue((int) value);
                // Enum values are reassigned in Frequency.cs so we have to match indexes
        }
    }
}