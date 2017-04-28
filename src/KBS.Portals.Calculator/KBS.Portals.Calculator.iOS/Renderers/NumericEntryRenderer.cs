using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using KBS.Portals.Calculator.CustomViews;
using KBS.Portals.Calculator.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FormattedEntry), typeof(NumericEntryRenderer))]
[assembly: ExportRenderer(typeof(NumericEntry), typeof(NumericEntryRenderer))]
namespace KBS.Portals.Calculator.iOS.Renderers
{
    public class NumericEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f))
            {
                Items = new[]
                {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate { Control.ResignFirstResponder(); })
                }
            };


            this.Control.InputAccessoryView = toolbar;
        }
    }
}