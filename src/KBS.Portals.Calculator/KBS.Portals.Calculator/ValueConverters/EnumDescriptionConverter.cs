using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Exceptions;
using KBS.Portals.Calculator.Logic.Util;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.ValueConverters
{
    class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Enum).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new TwoWayBindingNotSupportedException();
        }
    }
}
