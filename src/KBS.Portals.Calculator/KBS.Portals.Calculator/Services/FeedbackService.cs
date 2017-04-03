using System.Collections.Generic;
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
        public async Task<HttpResponseMessage> SendFeedback(string message, CalculatorModel calculatorModel = null)
        {
            Dictionary<string, string> customEventDictionary = Mapper.Map<Dictionary<string, string>>(calculatorModel);
            customEventDictionary.Add("User Feedback", message);

            var modelJson = JsonConvert.SerializeObject(calculatorModel, Formatting.Indented);
            return await Post(message, modelJson);
        }

        private async Task<HttpResponseMessage> Post(string message, string model)
        {
            // API Doc: https://support.hockeyapp.net/kb/api/api-feedback

            IApplicationService applicationService = FreshIOC.Container.Resolve<IApplicationService>();
            var appId = applicationService.AppId;
            string uri = "https://rink.hockeyapp.net/api/2/apps/" + appId + "/feedback";
            HttpClient client = new HttpClient();
            Dictionary<string, string> form = new Dictionary<string, string>();
            form["name"] = Device.OS + " User";
            form["subject"] = "In-app feedback" + (model == null ? "" : " - includes calculator data");
            form["text"] = message + "\n\n---------------\n\nCalculator data:\n\n" + model;
            return await client.PostAsync(uri, new FormUrlEncodedContent(form));
        }


    }
}