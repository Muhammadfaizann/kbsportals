using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace KBS.Portals.Calculator.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseAddress = "https://kbsportal.azurewebsites.net/core";
        private const string TokenEndpoint = BaseAddress + "/connect/token";

        public bool LogIn(string username, string password)
        {
            TokenResponse response = RequestToken(username, password);
            return response.AccessToken != null;
        }

        private static TokenResponse RequestToken(string username, string password)
        {
            var client = new TokenClient(TokenEndpoint, "KBS.Calculator", "3ZufOIEYhWT#!duEN8mB");

            return client.RequestResourceOwnerPasswordAsync(username, password, "openid all_claims").Result;
        }
    }
}
