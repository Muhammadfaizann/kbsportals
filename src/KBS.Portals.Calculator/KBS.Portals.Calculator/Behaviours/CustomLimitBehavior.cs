using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.CustomViews;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class CustomLimitBehavior : BaseEntryBehavior<NumericEntry>
    {
        private readonly decimal _lowerLimit;
        private readonly decimal _upperLimit;

        public CustomLimitBehavior(decimal lowerLimit, decimal upperLimit)
        {
            _lowerLimit = lowerLimit;
            _upperLimit = upperLimit;
        }

        protected override bool IsValid(NumericEntry entry)
        {
            return (entry.Value >= _lowerLimit) && (entry.Value <= _upperLimit);
        }
    }
}
