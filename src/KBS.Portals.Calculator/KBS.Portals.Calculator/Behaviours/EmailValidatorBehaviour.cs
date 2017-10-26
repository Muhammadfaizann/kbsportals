using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class EmailValidatorBehaviour : BaseEntryBehavior<Entry>
    {
        private const string EmailRegex =
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
            ;

        public override bool IsValid(Entry entry)
        {
            return !string.IsNullOrWhiteSpace(entry.Text) && Regex.IsMatch(entry.Text, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(1));
        }
    }
}
