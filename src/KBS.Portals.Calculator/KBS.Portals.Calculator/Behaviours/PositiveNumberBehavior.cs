using KBS.Portals.Calculator.CustomViews;

namespace KBS.Portals.Calculator.Behaviours
{
    class PositiveNumberBehavior : BaseEntryBehavior<NumericEntry>
    {
        public override bool IsValid(NumericEntry entry)
        {
            return entry.Value > 0;
        }
    }
}