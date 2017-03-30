﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBS.Portals.Calculator.Services
{
    public interface ISettingsService
    {
        string Username { get; set; }
        string Password { get; set; }
        bool RememberMe { get; set; }
        decimal APR { get; set; }
        decimal IRR { get; set; }
        decimal DocFee { get; set; }
        decimal PurFee { get; set; }
        int Term { get; set; }
    }
}
