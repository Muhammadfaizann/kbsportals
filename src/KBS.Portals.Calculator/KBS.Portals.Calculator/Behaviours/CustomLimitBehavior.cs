using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.CustomViews;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class CustomLimitBehavior : Behavior<NumericEntry>
    {
        public CustomLimitBehavior(double lowerLimit, double upperLimit)
        {
            
        }
    }
}
