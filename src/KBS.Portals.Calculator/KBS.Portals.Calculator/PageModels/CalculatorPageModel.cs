using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarouselView.FormsPlugin.Abstractions;
using FreshMvvm;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;

namespace KBS.Portals.Calculator.PageModels
{
    public class CalculatorPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        private ISettingsService _settingsService;
        private CalculatorModel _calculatorModel;
        public IList<CalculatorCarouselModel> PageModels { get; set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public CalculatorPageModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _calculatorModel = new CalculatorModel()
            {
                Product = Product.Lease,
                Frequency = Frequency.Monthly,
                StartDate = DateTime.Today,
                NextDate = DateTime.Today.AddMonths(1),
                APR = _settingsService.APR,
                IRR = _settingsService.IRR,
                DocFee = _settingsService.DocFee,
                PurFee = _settingsService.PurFee,
                Term = _settingsService.Term
            };

            PageModels = new List<CalculatorCarouselModel>
            {
                new CalculatorCarouselModel(CalculationType.APRInstallment, _calculatorModel),
                new CalculatorCarouselModel(CalculationType.IRRInstallment, _calculatorModel),
                new CalculatorCarouselModel(CalculationType.Rate, _calculatorModel)
            };
            Title = PageModels[0].CalculationType.ToString();
        }

        public Command Calculate
        {
            get
            {
                return new Command(() =>
                {
                    _settingsService.APR = _calculatorModel.APR;
                    _settingsService.IRR = _calculatorModel.IRR;
                    _settingsService.DocFee = _calculatorModel.DocFee;
                    _settingsService.PurFee = _calculatorModel.PurFee;
                    _settingsService.Term = _calculatorModel.Term;
                });
            }
        }

        public void OnPositionSelected(object sender, EventArgs e)
        {
            var carouselViewControl = (sender as CarouselViewControl);
            Title = PageModels[carouselViewControl.Position].CalculationType.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}