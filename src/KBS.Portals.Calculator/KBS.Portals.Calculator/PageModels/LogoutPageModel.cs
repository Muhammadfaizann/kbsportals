using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using KBS.Portals.Calculator.Enums;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.PageModels
{
    public class LogoutPageModel : FreshBasePageModel
    {
        private readonly ISettingsService _settingsService;

        public LogoutPageModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            _settingsService.AccessToken = null;
            CoreMethods.PushPageModelWithNewNavigation<LoginPageModel>(null);
        }
    }
}
