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
    class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? modelValue = (value as decimal?);
            return modelValue == null ? "" : string.Format("€{0:0,0.00}", modelValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string money = (value as string);
            if (money == null || money.Equals(""))
            {
                return 0.00m;
            }
            string validDecimal = Regex.Replace(money, "[,]", "");
            return decimal.Parse(validDecimal);
        }
    }
}
