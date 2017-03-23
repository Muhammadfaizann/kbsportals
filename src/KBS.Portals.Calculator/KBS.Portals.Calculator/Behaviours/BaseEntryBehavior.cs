using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    abstract class BaseEntryBehavior : Behavior<Entry>
    {
        protected Color InvalidColor = Color.FromHex("#FFEDED");
        protected Color ValidColor = Color.White;

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += CheckEntryIsValid;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= CheckEntryIsValid;
            entry.BackgroundColor = ValidColor;
            base.OnDetachingFrom(entry);
        }

        public void CheckEntryIsValid(object sender, TextChangedEventArgs args)
        {
            var entry = (sender as Entry);
            if (IsValid(entry))
            {
                Validate(entry);
            }
            else
            {
                Invalidate(entry);
            }
        }

        public void Validate(Entry entry)
        {
            entry.BackgroundColor = ValidColor;
        }

        public void Invalidate(Entry entry)
        {
            entry.BackgroundColor = InvalidColor;
        }

        protected abstract bool IsValid(Entry entry);
    }
}