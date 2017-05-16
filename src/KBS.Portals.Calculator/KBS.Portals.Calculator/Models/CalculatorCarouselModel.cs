using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Models
{
    public class CalculatorCarouselModel : INotifyPropertyChanged
    {
        private CalculationType _calculationType;

        public CalculationType CalculationType
        {
            get { return _calculationType; }
            set
            {
                if (value != _calculationType)
                {
                    _calculationType = value;
                    OnPropertyChanged();
                }
            }
        }

        private CalculatorModel _calculatorModel;

        public CalculatorModel CalculatorModel
        {
            get { return _calculatorModel; }
            set
            {
                if (value != _calculatorModel)
                {
                    _calculatorModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public CalculatorCarouselModel(CalculationType calculationType, CalculatorModel calculatorModel)
        {
            CalculationType = calculationType;
            CalculatorModel = calculatorModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}