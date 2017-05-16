using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KBS.Portals.Calculator.CustomViews;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class PositiveNumberBehavior : BaseEntryBehavior<NumericEntry>
    {
        protected override bool IsValid(NumericEntry entry)
        {
            return entry.Value > 0;
        }
    }
}