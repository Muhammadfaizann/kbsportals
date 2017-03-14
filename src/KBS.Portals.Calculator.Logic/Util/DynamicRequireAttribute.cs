using System;
using System.ComponentModel.DataAnnotations;
using KBS.Portals.Calculator.Logic.Enums;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic.Util
{
    public class DynamicRequireAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cd = (CalculatorData)validationContext.ObjectInstance;

            if (cd.CalculationType == CalculationType.APRInstallment)
            {
                if(cd.NoOfInstallments <= 1)
                    return new ValidationResult("A term of 1 or greater is required.");

                if (cd.APR <= 0 )
                    return new ValidationResult("A positive APR is required.");

                if (cd.FinanceAmount <= 0)
                    return new ValidationResult("A positive finance amount is required.");
            }
           if (cd.CalculationType == CalculationType.IRRInstallment)
            {
                if (cd.NoOfInstallments <= 1)
                    return new ValidationResult("A term of 1 or greater is required.");

                if (cd.IRR <= 0)
                    return new ValidationResult("A positive IRR is required.");

                if (cd.FinanceAmount <= 0)
                    return new ValidationResult("A positive finance amount is required.");
            }

            return ValidationResult.Success;
        }
    }
}