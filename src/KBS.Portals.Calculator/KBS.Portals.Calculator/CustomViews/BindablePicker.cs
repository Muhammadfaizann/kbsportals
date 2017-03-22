using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Enums;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.CustomViews
{
    public class BindablePicker : Picker
    {

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(BindablePicker), null, BindingMode.TwoWay,
            propertyChanged: (s, o, n) => OnItemsSourcePropertyChanged(s, (IEnumerable) o, (IEnumerable) n));

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem),
            typeof(object),
            typeof(BindablePicker), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);

        public BindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = ItemsSource[SelectedIndex];
            }
        }
        
        public IList ItemsSource
        {
            get { return (IList) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        
        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable value,
            IEnumerable newValue)
        {
            var picker = (BindablePicker) bindable;
            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        foreach (var newItem in args.NewItems)
                        {
                            picker.Items.Add((newItem ?? "").ToString());
                        }
                    }
                    if (args.OldItems != null)
                    {
                        foreach (var oldItem in args.OldItems)
                        {
                            picker.Items.Remove((oldItem ?? "").ToString());
                        }
                    }
                };
            }

            if (newValue == null)
                return;

            picker.Items.Clear();

            foreach (var item in newValue)
                picker.Items.Add((item ?? "").ToString());
        }

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue)
        {
            var picker = (BindablePicker) bindable;
            if (picker.ItemsSource != null)
                picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);
        }
    }
}