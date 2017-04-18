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
        }
    }
}
