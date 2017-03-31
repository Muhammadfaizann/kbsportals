﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBS.Portals.Calculator.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LogIn(string username, string password);
    }
}