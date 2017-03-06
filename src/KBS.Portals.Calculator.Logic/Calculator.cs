using System.Collections.Generic;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public abstract class Calculator : ICalculator
    {
        protected CalculatorData Input { get; set; }
        public List<string> ErrorMessages { get; set; }

        protected Calculator(CalculatorData input)
        {
            Input = input;
            ErrorMessages = new List<string>();
        }

        public abstract CalculatorData Calculate();

        public void Reload(CalculatorData input)
        {
            Input = input;
        }
    }
}