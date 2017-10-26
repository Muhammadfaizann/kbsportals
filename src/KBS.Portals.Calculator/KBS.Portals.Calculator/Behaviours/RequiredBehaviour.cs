using System;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class RequiredBehaviour : BaseEntryBehavior<Entry>
    {
        public override bool IsValid(Entry entry)
        {
            return !string.IsNullOrWhiteSpace(entry.Text);
        }
    }
}
