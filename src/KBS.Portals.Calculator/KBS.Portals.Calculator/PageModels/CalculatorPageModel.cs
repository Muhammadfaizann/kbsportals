using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarouselView.FormsPlugin.Abstractions;
using FreshMvvm;
using KBS.Portals.Calculator.Logic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.Pages;
using KBS.Portals.Calculator.Services;
using Xamarin.Forms;
using AutoMapper;
using KBS.Portals.Calculator.Views;

namespace KBS.Portals.Calculator.PageModels
{
    public class CalculatorPageModel : FreshBasePageModel, INotifyPropertyChanged
    {
        private readonly ISettingsService _settingsService;
        public CalculatorModel CalculatorModel { get; set; }
        public IList<CalculatorCarouselModel> PageModels { get; set; }
        private CalculationType _title;
        public CalculationType Title
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
            CalculatorModel = new CalculatorModel()
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
                new CalculatorCarouselModel(CalculationType.APRInstallment, CalculatorModel),
                new CalculatorCarouselModel(CalculationType.IRRInstallment, CalculatorModel),
                new CalculatorCarouselModel(CalculationType.Rate, CalculatorModel)
            };
            Title = PageModels[0].CalculationType;
            NavContainer nav = FreshIOC.Container.Resolve<NavContainer>();
            nav.FeedbackItem.Clicked += (sender, args) =>
            {
                CoreMethods.PushPageModelWithNewNavigation<FeedbackPageModel>(CalculatorModel);
            };
        }

        public Command Calculate
        {
            get
            {
                return new Command(() =>
                {
                    SaveLastUsedValues();
                    CalculatorData backendDataModel = Mapper.Map<CalculatorData>(CalculatorModel);
                    ICalculator calculator = CalculatorFactory.Create(Title, backendDataModel);
                    var resultData = calculator.Calculate();
                    CalculatorModel resultModel = Mapper.Map<CalculatorModel>(resultData);
                    Mapper.Map(resultModel, CalculatorModel); // Call the set methods to trigger bindings
                    CalculatorModel.IsDirty = false;
                });
            }
        }

        private void SaveLastUsedValues()
        {
            _settingsService.APR = CalculatorModel.APR;
            _settingsService.IRR = CalculatorModel.IRR;
            _settingsService.DocFee = CalculatorModel.DocFee;
            _settingsService.PurFee = CalculatorModel.PurFee;
            _settingsService.Term = CalculatorModel.Term;
        }

        public void OnPositionSelected(object sender, EventArgs e)
        {
            var carouselViewControl = (sender as CarouselViewControl);
            Title = PageModels[carouselViewControl.Position].CalculationType;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}