using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            e.NewElement.PropertyChanged += ElementOnPropertyChanged;

            this.Control.InputAccessoryView = toolbar;
        }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var textColor = Element.TextColor.ToUIColor();
            Control.TextColor = Element.IsEnabled ? textColor : textColor.ColorWithAlpha(0.6f);
            Control.BackgroundColor = Element.IsEnabled ? UIColor.Clear : UIColor.Black.ColorWithAlpha(0.025f);
        }
    }
}