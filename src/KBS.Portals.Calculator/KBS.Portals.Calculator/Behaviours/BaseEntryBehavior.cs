using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    public abstract class BaseEntryBehavior<T> : Behavior<T> where T: Entry
    {
        protected Color InvalidColor = Color.FromHex("#FFEDED");
        protected Color ValidColor = Color.White;

        protected override void OnAttachedTo(T entry)
        {
            entry.TextChanged += CheckEntryIsValid;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(T entry)
        {
            entry.TextChanged -= CheckEntryIsValid;
            entry.BackgroundColor = ValidColor;
            base.OnDetachingFrom(entry);
        }

        public void CheckEntryIsValid(object sender, TextChangedEventArgs args)
        {
            var entry = (sender as T);
            if (IsValid(entry))
            {
                Validate(entry);
            }
            else
            {
                Invalidate(entry);
            }
        }

        public void Validate(T entry)
        {
            entry.BackgroundColor = ValidColor;
        }

        public void Invalidate(T entry)
        {
            entry.BackgroundColor = InvalidColor;
        }

        protected abstract bool IsValid(T entry);
    }
}