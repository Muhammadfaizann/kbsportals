using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.ValueConverters
{
    class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? modelValue = (value as decimal?);
            return modelValue == null ? "" : string.Format("{0:P}", modelValue / 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string percent = (value as string);
            if (percent == null || percent.Equals(""))
            {
                return 0.00m;
            }
            string validDecimal = Regex.Replace(percent, "[,]", "");
            return decimal.Parse(validDecimal);
        }
    }
}
