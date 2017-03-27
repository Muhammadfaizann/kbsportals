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
                _product = value;
                OnPropertyChanged();
            }
        }

        private decimal _financeAmount;

        public decimal FinanceAmount
        {
            get { return _financeAmount; }
            set
            {
                _financeAmount = value;
                OnPropertyChanged();
            }
        }

        private decimal _commission;

        public decimal Commission
        {
            get { return _commission; }
            set
            {
                _commission = value;
                OnPropertyChanged();
            }
        }

        private int _upFrontNo;

        public int UpFrontNo
        {
            get { return _upFrontNo; }
            set
            {
                _upFrontNo = value;
                OnPropertyChanged();
            }
        }

        private decimal _upFrontValue;

        public decimal UpFrontValue
        {
            get { return _upFrontValue; }
            set
            {
                _upFrontValue = value;
                OnPropertyChanged();
            }
        }

        private decimal _aPR;

        public decimal APR
        {
            get { return _aPR; }
            set
            {
                _aPR = value;
                OnPropertyChanged();
            }
        }

        private decimal _iRR;

        public decimal IRR
        {
            get { return _iRR; }
            set
            {
                _iRR = value;
                OnPropertyChanged();
            }
        }

        private decimal _docFee;

        public decimal DocFee
        {
            get { return _docFee; }
            set
            {
                _docFee = value;
                OnPropertyChanged();
            }
        }

        private decimal _purFee;

        public decimal PurFee
        {
            get { return _purFee; }
            set
            {
                _purFee = value;
                OnPropertyChanged();
            }
        }

        private decimal _ballon;

        public decimal Ballon
        {
            get { return _ballon; }
            set
            {
                _ballon = value;
                OnPropertyChanged();
            }
        }

        private decimal _residual;

        public decimal Residual
        {
            get { return _residual; }
            set
            {
                _residual = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _nextDate;

        public DateTime NextDate
        {
            get { return _nextDate; }
            set
            {
                _nextDate = value;
                OnPropertyChanged();
            }
        }

        private Frequency _frequency;

        public Frequency Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                OnPropertyChanged();
            }
        }

        private decimal _installment;

        public decimal Installment
        {
            get { return _installment; }
            set
            {
                _installment = value;
                OnPropertyChanged();
            }
        }

        private decimal _charges;

        public decimal Charges
        {
            get { return _charges; }
            set
            {
                _charges = value;
                OnPropertyChanged();
            }
        }

        private int _term;

        public int Term
        {
            get { return _term; }
            set
            {
                _term = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalCost;

        public decimal TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalSchedule;

        public decimal TotalSchedule
        {
            get { return _totalSchedule; }
            set
            {
                _totalSchedule = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}