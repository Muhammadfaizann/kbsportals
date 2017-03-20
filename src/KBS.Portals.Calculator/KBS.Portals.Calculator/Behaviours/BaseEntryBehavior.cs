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
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            entry.BackgroundColor = ValidColor;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (sender as Entry);
            bool isValid = IsValid(entry);
            entry.BackgroundColor = isValid ? ValidColor : InvalidColor;
        }

        protected abstract bool IsValid(Entry entry);
    }
}
