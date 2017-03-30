using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KBS.Portals.Calculator.Logic.Enums
{
    public enum CalculationType
    {
        [Display(Description = "APR Installment")]
        APRInstallment,
        [Display(Description = "IRR Installment")]
        IRRInstallment,
        [Display(Description = "Rate")]
        Rate,
        [Display(Description = "Finance Amount")]
        FinanceAmount,
        [Display(Description = "Term")]
        Term,
        [Display(Description = "Balloon Residual")]
        BalRes,
        [Display(Description = "Commission")]
        Commission
    }
}