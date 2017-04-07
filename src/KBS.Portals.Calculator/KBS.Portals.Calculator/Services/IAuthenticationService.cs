using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBS.Portals.Calculator.Services
{
    public interface IAuthenticationService
    {
        Task<string> LogIn(string username, string password);
        Task<bool> ValidateToken(string token);
    }
}
