﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Behaviours;
using KBS.Portals.Calculator.CustomViews;
using KBS.Portals.Calculator.Logic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.ValueConverters;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.Views
{
    public partial class CalculatorView : ContentView
    {
        private readonly PositiveNumberBehavior _positiveNumberBehavior;

        private static readonly IList<CalculationType> CalculationTypesToEnableUpFrontValueFor = new[]
        {CalculationType.Rate, CalculationType.NoOfInstallments, CalculationType.BalRes, CalculationType.Commission};

        private static readonly IList<CalculationType> CalculationTypesToDisableAPRIRRFor = new[]
        {CalculationType.FinanceAmount, CalculationType.NoOfInstallments, CalculationType.BalRes, CalculationType.Commission};

        private static readonly IList<CalculationType> CalculationTypesToTogglePurFeeOnHirePurchaseFor = new[]
        {CalculationType.APRInstallment, CalculationType.IRRInstallment, CalculationType.Rate};

        public CalculatorView()
        {
            InitializeComponent();
            _positiveNumberBehavior = new PositiveNumberBehavior();
            ProductPicker.SelectedIndexChanged += ProductPickerOnSelectedIndexChanged;
            UpFrontNo.TextChanged += UpFrontNoOnTextChanged;
            APR.PropertyChanged += AprOnTextChanged;
            IRR.PropertyChanged += IrrOnTextChanged;
            ProductPicker.ItemsSource = Enum.GetValues(typeof(Product));
            FrequencyPicker.ItemsSource = Enum.GetValues(typeof(Frequency));
        }

        private void AprOnTextChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName.Equals(nameof(FormattedEntry.Value)))
            {
                var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
                if (calculatorCarouselModel != null && CalculationTypesToDisableAPRIRRFor.Contains(calculatorCarouselModel.CalculationType))
                {
                    decimal aprPercentage = ((FormattedEntry) sender).Value;
                    //IRR.IsEnabled = aprPercentage <= 0m;
                }
            }
        }

        private void IrrOnTextChanged(object sender, PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName.Equals(nameof(FormattedEntry.Value)))
            {
                var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
                if (calculatorCarouselModel != null && CalculationTypesToDisableAPRIRRFor.Contains(calculatorCarouselModel.CalculationType))
                {
                    decimal irrPercentage = ((FormattedEntry)sender).Value;
                    //APR.IsEnabled = irrPercentage <= 0m;
                }
            }
        }

        private void UpFrontNoOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
            if (calculatorCarouselModel != null)
            {
                int value;
                int.TryParse(textChangedEventArgs.NewTextValue, out value);
                UpFrontValue.IsEnabled = value > 0  && CalculationTypesToEnableUpFrontValueFor.Contains(calculatorCarouselModel.CalculationType);
            }
        }

        private void ProductPickerOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
            if (calculatorCarouselModel != null)
            {
                var calcType = calculatorCarouselModel.CalculationType;
                if (CalculationTypesToTogglePurFeeOnHirePurchaseFor.Contains(calcType))
                {
                    Product product = (Product) ProductPicker.SelectedItem;
                    if (product == Product.HirePurchase)
                    {
                        PurFee.Behaviors.Add(_positiveNumberBehavior);
                        _positiveNumberBehavior.CheckEntryIsValid(PurFee, null);
                    }
                    else
                    {
                        PurFee.Behaviors.Remove(_positiveNumberBehavior);
                        _positiveNumberBehavior.Validate(PurFee);
                    }
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            var calculatorCarouselModel = BindingContext as CalculatorCarouselModel;
            if (calculatorCarouselModel != null)
            {
                var calcType = calculatorCarouselModel.CalculationType;
                FinanceAmount.IsEnabled = calcType != CalculationType.FinanceAmount;
                NoOfInstallments.IsEnabled = calcType != CalculationType.NoOfInstallments;
                APR.IsEnabled = calcType == CalculationType.APRInstallment;
                IRR.IsEnabled = !(calcType == CalculationType.Rate || calcType == CalculationType.APRInstallment);
                Installment.IsEnabled = !(calcType == CalculationType.IRRInstallment || calcType == CalculationType.APRInstallment);
                Charges.IsEnabled = calcType == CalculationType.BalRes || calcType == CalculationType.Commission;
                Commission.IsEnabled = !(calcType == CalculationType.Commission || calcType == CalculationType.BalRes);
                Balloon.IsEnabled = calcType != CalculationType.BalRes;
            }
            base.OnBindingContextChanged();
        }
    }
}