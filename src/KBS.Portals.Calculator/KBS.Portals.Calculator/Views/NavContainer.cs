using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using KBS.Portals.Calculator.PageModels;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Views
{
    public class NavContainer : FreshMasterDetailNavigationContainer
    {
        public ToolbarItem FeedbackItem { get; set; }

        public NavContainer(string navigationServiceName) : base(navigationServiceName)
        {
            FreshIOC.Container.Register(this);
            FeedbackItem = new ToolbarItem("Feedback", "feedback_icon", () => { });
            AddPage<CalculatorPageModel>("Calculate");
            AddPage<LogoutPageModel>("Log out");
            ((NavigationPage)Detail).Style = MainContainerStyle();
            Init("Menu");
            Master.ToolbarItems.Add(FeedbackItem);
        }

        private Style MainContainerStyle()
        {
            Style style = new Style(typeof(NavigationPage));
            style.Setters.Add(new Setter()
            {
                Property = NavigationPage.BarBackgroundColorProperty,
                Value = Color.FromHex("#FFFFFF")
            });
            return style;
        }
    }
}
