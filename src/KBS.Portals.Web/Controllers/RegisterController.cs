using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using KBS.Portals.Web.Services;
using Newtonsoft.Json;

namespace KBS.Portals.Web.Controllers
{
    public class RegisterController : ApiController
    {
        // GET: api/Default
        public HttpResponseMessage Post(HttpRequestMessage recordRequest)
        {
            var registerToName = WebConfigurationManager.AppSettings["RegisterToName"];
            var registerToAddress = WebConfigurationManager.AppSettings["RegisterToAddress"];
            var emailSenderName = WebConfigurationManager.AppSettings["EmailSenderName"];
            var emailSenderAddress = WebConfigurationManager.AppSettings["EmailSenderAddress"];
            var dataRequest = recordRequest.Content.ReadAsStringAsync().Result;
            Dictionary<string, string> record = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataRequest);

            using (var emailService = new EmailService())
            {
                var body = String.Format("Name: {0}, Email {1}", record["Name"], record["Email"]); 
                if (emailService.Send(registerToName, registerToAddress, emailSenderName, 
                    emailSenderAddress, "Register request", body))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
