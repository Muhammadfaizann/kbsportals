using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class PositiveNumberBehavior : BaseEntryBehavior
    {
        protected override bool IsValid(Entry entry)
        {
            decimal parsedDecimal;
            decimal.TryParse(entry.Text, out parsedDecimal);
            return parsedDecimal > 0;
        }
    }
}
