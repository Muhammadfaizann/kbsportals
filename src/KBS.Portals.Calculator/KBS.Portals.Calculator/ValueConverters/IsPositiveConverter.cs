using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Exceptions;
using KBS.Portals.Calculator.Logic.Enums;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.ValueConverters
{
    class IsPositiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as int?) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new TwoWayBindingNotSupportedException();
        }
    }
}
