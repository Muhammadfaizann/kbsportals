using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.CustomViews;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class PositiveNumberBehavior : BaseEntryBehavior
    {
        protected override bool IsValid(Entry entry)
        {
            decimal parsedDecimal;
            if (entry is FormattedEntry)
            {
                parsedDecimal = ((FormattedEntry) entry).Value;
            }
            else
            {
                decimal.TryParse(entry.Text, out parsedDecimal);
            }
            return parsedDecimal > 0;
        }
    }
}
