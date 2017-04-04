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

        private bool _isCollapsed;

        public bool IsCollapsed
        {
            get { return _isCollapsed; }
            set
            {
                _isCollapsed = value;
                ToggleCollapse();
            }
        }

        private static readonly string EXPAND_IMAGE_SOURCE = "expand.png";
        private static readonly string COLLAPSE_IMAGE_SOURCE = "collapse.png";
        private readonly StackLayout _titleBar;
        private readonly Image _expandIcon;
        private readonly Label _titleLabel;

        public CollapsibleLayout()
        {
            _expandIcon = new Image
            {
                Source = COLLAPSE_IMAGE_SOURCE,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 20,
                WidthRequest = 20
            };
            _titleLabel = new Label
            {
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("#cf6733")
            };
            _titleBar = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { _titleLabel, _expandIcon }
            };
            VerticalOptions = LayoutOptions.Start;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Children.Add(_titleBar);
            var tapGestureRecognizer = new TapGestureRecognizer {Command = new Command(OnTitleClicked)};
            _titleBar.GestureRecognizers.Add(tapGestureRecognizer);
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            ((VisualElement) child).IsVisible = !IsCollapsed;
        }

        private void OnTitleClicked()
        {
            IsCollapsed = !IsCollapsed;
        }

        private void ToggleCollapse()
        {
            Children.ToList().FindAll(v => v != _titleBar).ForEach(v => v.IsVisible = !v.IsVisible);
            _expandIcon.Source = IsCollapsed ? EXPAND_IMAGE_SOURCE : COLLAPSE_IMAGE_SOURCE;
        }
    }
}