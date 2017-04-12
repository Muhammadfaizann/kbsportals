using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FreshMvvm;
using IdentityModel.Client;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseAddress = "https://kbscalculatorportals.azurewebsites.net/core";
        private const string TokenEndpoint = BaseAddress + "/connect/token";
        private const string TokenValidationEndpoint = BaseAddress + "/connect/accesstokenvalidation?token=";

        public async Task<string> LogIn(string username, string password)
        {
            var client = new TokenClient(TokenEndpoint, "KBS.Calculator", "3ZufOIEYhWT#!duEN8mB");
            var token = await client.RequestResourceOwnerPasswordAsync(username, password, "openid all_claims");
            return token?.AccessToken;
        }

        public async Task<bool> ValidateToken(string token)
        {
            HttpClient client = new HttpClient();
            var httpResponseMessage = await client.GetAsync(TokenValidationEndpoint + token);
            return httpResponseMessage.StatusCode == HttpStatusCode.OK;
        }
    }
}
