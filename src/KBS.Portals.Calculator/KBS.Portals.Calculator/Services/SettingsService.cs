using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KBS.Portals.Calculator.Services
{
    public class SettingsService : ISettingsService
    {
        private static ISettings Settings => CrossSettings.Current;

        private readonly string UsernameKey = "username";
        private readonly string UsernameDefaultValue = string.Empty;

        private readonly string PasswordKey = "password";
        private readonly string PasswordDefaultValue = string.Empty;

        public string Username
        {
            get { return Settings.GetValueOrDefault(UsernameKey, UsernameDefaultValue); }
            set { Settings.AddOrUpdateValue(UsernameKey, value); }
        }

        public string Password
        {
            get { return Settings.GetValueOrDefault(PasswordKey, PasswordDefaultValue); }
            set { Settings.AddOrUpdateValue(PasswordKey, value); }
        }
    }
}
