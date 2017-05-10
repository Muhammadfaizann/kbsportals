﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KBS.Portals.Calculator.CustomViews;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Behaviours
{
    class PositiveNumberBehavior : BaseEntryBehavior<FormattedEntry>
    {
        protected override bool IsValid(FormattedEntry entry)
        {
            return entry.Value > 0;
        }
    }
}