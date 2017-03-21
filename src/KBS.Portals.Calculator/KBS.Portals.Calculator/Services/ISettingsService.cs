using System;
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
    }
}
