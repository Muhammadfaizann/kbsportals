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
    class PositiveNumberBehavior : BaseEntryBehavior
    {

        protected override bool IsValid(Entry entry)
        {
            var matchCollection = Regex.Matches(entry.Text ?? "", "-?\\d+.?\\d*");
            if (matchCollection.Count > 0)
            {
                string stringToParse = matchCollection[0].Value;
                decimal parsedDecimal;
                decimal.TryParse(stringToParse, out parsedDecimal);
                return parsedDecimal > 0;
            }
            return false;
        }
    }
}
