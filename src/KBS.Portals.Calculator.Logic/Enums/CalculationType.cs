using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KBS.Portals.Calculator.Logic.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
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
        [Display(Description = "No. of Ins.")]
        NoOfInstallments,
        [Display(Description = "Balloon Residual")]
        BalRes,
        [Display(Description = "Commission")]
        Commission
    }
}