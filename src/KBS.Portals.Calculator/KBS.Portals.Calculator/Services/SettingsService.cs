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

        private readonly string APRKey = "apr";
        private readonly decimal APRDefaultValue = 0.0m;

        private readonly string IRRKey = "irr";
        private readonly decimal IRRDefaultValue = 0.0m;

        private readonly string DocFeeKey = "docfee";
        private readonly decimal DocFeeDefaultValue = 0.0m;

        private readonly string PurFeeKey = "purfee";
        private readonly decimal PurFeeDefaultValue = 0.0m;

        private readonly string TermKey = "term";
        private readonly int TermDefaultValue = 0;

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
        public decimal APR
        {
            get { return Settings.GetValueOrDefault(APRKey, APRDefaultValue); }
            set { Settings.AddOrUpdateValue(APRKey, value); }
        }

        public decimal IRR
        {
            get { return Settings.GetValueOrDefault(IRRKey, IRRDefaultValue); }
            set { Settings.AddOrUpdateValue(IRRKey, value); }
        }
        public decimal DocFee
        {
            get { return Settings.GetValueOrDefault(DocFeeKey, DocFeeDefaultValue); }
            set { Settings.AddOrUpdateValue(DocFeeKey, value); }
        }

        public decimal PurFee
        {
            get { return Settings.GetValueOrDefault(PurFeeKey, PurFeeDefaultValue); }
            set { Settings.AddOrUpdateValue(PurFeeKey, value); }
        }
        public int Term
        {
            get { return Settings.GetValueOrDefault(TermKey, TermDefaultValue); }
            set { Settings.AddOrUpdateValue(TermKey, value); }
        }
    }
}
