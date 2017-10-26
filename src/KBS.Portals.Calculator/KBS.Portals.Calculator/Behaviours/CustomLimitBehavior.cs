using KBS.Portals.Calculator.CustomViews;

namespace KBS.Portals.Calculator.Behaviours
{
    class CustomLimitBehavior : BaseEntryBehavior<NumericEntry>
    {
        private decimal? _lowerLimit;

        public string LowerLimit
        {
            get { return _lowerLimit?.ToString(); }
            set
            {
                if (value != _lowerLimit?.ToString())
                {
                    decimal decimalValue;
                    _lowerLimit = decimal.TryParse(value, out decimalValue) ? decimalValue : (decimal?) null;
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _upperLimit;

        public string UpperLimit
        {
            get { return _upperLimit?.ToString(); }
            set
            {
                if (value != _upperLimit?.ToString())
                {
                    decimal decimalValue;
                    _upperLimit = decimal.TryParse(value, out decimalValue) ? decimalValue : (decimal?) null;
                    OnPropertyChanged();
                }
            }
        }

        public override bool IsValid(NumericEntry entry)
        {
            return (_lowerLimit == null || entry.Value >= _lowerLimit) && (_upperLimit == null || entry.Value <= _upperLimit);
        }
    }
}