using System;
using System.Net.Mail;
using System.Web.Configuration;

namespace KBS.Portals.Web.Services
{
    public class EmailService : IDisposable
    {
        private readonly SmtpClient _client;

        public EmailService()
        {
            var smtpHost = WebConfigurationManager.AppSettings["SmtpHost"];
            var smtpPort = int.Parse(WebConfigurationManager.AppSettings["SmtpPort"]);
            var smtpEnableSsl = bool.Parse(WebConfigurationManager.AppSettings["SmtpEnableSSl"]);
            var smtpUser = WebConfigurationManager.AppSettings["SmtpUser"];
            var smtpPassword = WebConfigurationManager.AppSettings["SmtpPassword"];

            _client = new SmtpClient
            {
                Host = smtpHost,
                Port = smtpPort,
                EnableSsl = smtpEnableSsl,

                Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,                
                UseDefaultCredentials = false,
            };
        }

        public bool Send(string toName, string toAddress, string fromName, string fromAddress,
            string subject, string body)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(toAddress, toName));
            msg.From = new MailAddress(fromAddress, fromName);
            msg.Subject = subject;
            msg.Body = body;

            try
            {
                _client.Send(msg);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}