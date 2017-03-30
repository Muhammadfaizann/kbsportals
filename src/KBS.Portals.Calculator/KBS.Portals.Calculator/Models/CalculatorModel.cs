using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Models
{
    public class CalculatorModel : INotifyPropertyChanged
    {
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

        private int _term;

        public int Term
        {
            get { return _term; }
            set
            {
                if (_term != value)
                {
                    _term = value;
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

        private bool _isDirty;

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null && !propertyName.Equals(nameof(IsDirty)))
            {
                IsDirty = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}