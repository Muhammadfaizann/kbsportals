using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Input;
using AutoMapper.Configuration.Conventions;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.CustomViews
{
    public class CollapsibleLayout : StackLayout
    {
        public string Title
        {
            get { return _titleLabel.Text; }
            set { _titleLabel.Text = value; }
        }

        private readonly Label _titleLabel;

        public CollapsibleLayout()
        {
            _titleLabel = new Label
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 30
            };
            Children.Add(_titleLabel);
            var tapGestureRecognizer = new TapGestureRecognizer {Command = TapCommand};
            _titleLabel.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private ICommand TapCommand => new Command(OnTitleClicked);

        public void OnTitleClicked()
        {
            Children.ToList().FindAll(v => v != _titleLabel).ForEach(v => v.IsVisible = !v.IsVisible);
        }
    }
}