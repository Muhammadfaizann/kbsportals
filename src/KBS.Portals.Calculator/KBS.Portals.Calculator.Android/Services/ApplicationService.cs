using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Droid.Services
{
    public class ApplicationService : IApplicationService
    {
        public void Quit()
        {
            var activity = (Activity) Forms.Context;
            activity.FinishAffinity();
        }

        public string AppId => MainActivity.AppId;
    }
}