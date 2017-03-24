using System;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace KBS.Portals.Calculator.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseAddress = "https://kbsportal.azurewebsites.net/core";
        private const string TokenEndpoint = BaseAddress + "/connect/token";

        public async Task<bool> LogIn(string username, string password)
        {
            var client = new TokenClient(TokenEndpoint, "KBS.Calculator", "3ZufOIEYhWT#!duEN8mB");
            var token = await client.RequestResourceOwnerPasswordAsync(username, password, "openid all_claims");
            return token?.AccessToken != null;
        }
    }
}
