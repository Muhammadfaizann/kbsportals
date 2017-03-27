using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using KBS.Portals.Calculator.Services;
using UIKit;

namespace KBS.Portals.Calculator.iOS.Services
{
    class QuitApplicationService : IQuitApplicationService
    {
        public void Quit()
        {
            Thread.CurrentThread.Abort();
        }
    }
}