using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Enums;

namespace KBS.Portals.Calculator.Models
{
    public class CalculatorCarouselModel
    {
        public CalculationType CalculationType { get; set; }
        public CalculatorModel CalculatorModel { get; set; }

        public CalculatorCarouselModel(CalculationType calculationType, CalculatorModel calculatorModel)
        {
            CalculationType = calculationType;
            CalculatorModel = calculatorModel;
        }
    }
}
