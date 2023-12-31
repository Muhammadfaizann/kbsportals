﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FreshMvvm;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Services;

namespace KBS.Portals.Calculator.Models
{
    public class CalculatorModel : INotifyPropertyChanged
    {
        public void Init()
        {
            var settingsService = FreshIOC.Container.Resolve<ISettingsService>();

            Product = Product.Lease;
            FinanceAmount = 0;
            Frequency = Frequency.Monthly;
            NoOfInstallments = settingsService.NoOfInstallments;
            APR = settingsService.APR;
            IRR = settingsService.IRR;
            Installment = 0;

            StartDate = DateTime.Today;
            NextDate = DateTime.Today.AddMonths(1);

            UpFrontNo = 0;
            UpFrontValue = 0;
            DocFee = settingsService.DocFee;
            Commission = 0;
            PurFee = settingsService.PurFee;
            Ballon = 0;
            Residual = 0;

            Charges = 0;
            TotalCost = 0;
            TotalSchedule = 0;
        }

        public CalculatorModel()
        {
            Init();
        }

        private Product _product;

        public Product Product
        {
            get { return _product; }
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _financeAmount;

        public decimal FinanceAmount
        {
            get { return _financeAmount; }
            set
            {
                if (_financeAmount != value)
                {
                    _financeAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _commission;

        public decimal Commission
        {
            get { return _commission; }
            set
            {
                if (_commission != value)
                {
                    _commission = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _upFrontNo;

        public int UpFrontNo
        {
            get { return _upFrontNo; }
            set
            {
                if (_upFrontNo != value)
                {
                    _upFrontNo = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _upFrontValue;

        public decimal UpFrontValue
        {
            get { return _upFrontValue; }
            set
            {
                if (_upFrontValue != value)
                {
                    _upFrontValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _aPR;

        public decimal APR
        {
            get { return _aPR; }
            set
            {
                if (_aPR != value)
                {
                    _aPR = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _iRR;

        public decimal IRR
        {
            get { return _iRR; }
            set
            {
                if (_iRR != value)
                {
                    _iRR = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _docFee;

        public decimal DocFee
        {
            get { return _docFee; }
            set
            {
                if (_docFee != value)
                {
                    _docFee = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _purFee;

        public decimal PurFee
        {
            get { return _purFee; }
            set
            {
                if (_purFee != value)
                {
                    _purFee = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _ballon;

        public decimal Ballon
        {
            get { return _ballon; }
            set
            {
                if (_ballon != value)
                {
                    _ballon = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _residual;

        public decimal Residual
        {
            get { return _residual; }
            set
            {
                if (_residual != value)
                {
                    _residual = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _nextDate;

        public DateTime NextDate
        {
            get { return _nextDate; }
            set
            {
                if (_nextDate != value)
                {
                    _nextDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private Frequency _frequency;

        public Frequency Frequency
        {
            get { return _frequency; }
            set
            {
                if (_frequency != value)
                {
                    _frequency = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _installment;

        public decimal Installment
        {
            get { return _installment; }
            set
            {
                if (_installment != value)
                {
                    _installment = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _charges;

        public decimal Charges
        {
            get { return _charges; }
            set
            {
                if (_charges != value)
                {
                    _charges = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _noOfInstallments;

        public int NoOfInstallments
        {
            get { return _noOfInstallments; }
            set
            {
                if (_noOfInstallments != value)
                {
                    _noOfInstallments = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _totalCost;

        public decimal TotalCost
        {
            get { return _totalCost; }
            set
            {
                if (_totalCost != value)
                {
                    _totalCost = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _totalSchedule;

        public decimal TotalSchedule
        {
            get { return _totalSchedule; }
            set
            {
                if (_totalSchedule != value)
                {
                    _totalSchedule = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}