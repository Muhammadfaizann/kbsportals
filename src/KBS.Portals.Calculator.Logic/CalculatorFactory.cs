using System;
using System.Collections.Generic;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public static class CalculatorFactory
    {
        private static Dictionary<CalculationType, Type> _types = new Dictionary<CalculationType, Type>
        {
            {CalculationType.APRInstallment, typeof(APRInstallmentCalculator) }
        };

        public static ICalculator Create(CalculationType type, CalculatorData input)
        {
            //TODO replace with DI code

            if (type == CalculationType.APRInstallment)
            {
                return new APRInstallmentCalculator(input);
            }

            throw new ArgumentException($"Type {type} is not supported");
        }
    }
}