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
            {CalculationType.APRInstallment, typeof(InstallmentApr) }
        };

        public static ICalculator Create(CalculationType type, CalculatorData input)
        {
            //TODO replace with DI code

            input.CalculationType = type;
            switch (type)
            {
                case CalculationType.APRInstallment:
                    return new InstallmentApr(input);
                case CalculationType.IRRInstallment:
                    return new InstallmentIrr(input);
                case CalculationType.Rate:
                    return new Rate(input);
                case CalculationType.FinanceAmount:
                    return new FinanceAmount(input);
                case CalculationType.Term:
                    return new Term(input);
                case CalculationType.BalRes:
                    return new BalRes(input);
                case CalculationType.Commission:
                    return new Commission(input);
            }

            throw new ArgumentException($"Type {type} is not supported");
        }
    }
}