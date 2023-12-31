using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FreshMvvm;
using KBS.Portals.Calculator.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Services
{
    public class FeedbackService : IFeedbackService
    {
        private static readonly string FEEDBACK_API = "https://rink.hockeyapp.net/api/2/apps/{0}/feedback";

        // API Doc: https://support.hockeyapp.net/kb/api/api-feedback
        public async Task<bool> SendFeedback(string message, CalculatorModel calculatorModel = null)
        {
            var modelJson = JsonConvert.SerializeObject(calculatorModel, Formatting.Indented);
            ISettingsService settingsService = FreshIOC.Container.Resolve<ISettingsService>();
            IApplicationService applicationService = FreshIOC.Container.Resolve<IApplicationService>();
            var appId = applicationService.AppId;
            string uri = string.Format(FEEDBACK_API, appId);
            HttpClient client = new HttpClient();
            Dictionary<string, string> form = new Dictionary<string, string>
            {
                ["name"] = string.Format("{0} (Platform: {1})", settingsService.Username, Device.RuntimePlatform),
                ["subject"] = "In-app feedback" + (calculatorModel == null ? "" : " - includes calculator data"),
                ["text"] = message + (calculatorModel == null ? "" : "\n\n---------------\n\nCalculator data:\n\n" + modelJson)
            };
            var httpResponseMessage = await client.PostAsync(uri, new FormUrlEncodedContent(form));
            return httpResponseMessage.StatusCode == HttpStatusCode.Created;
        }
    }
}