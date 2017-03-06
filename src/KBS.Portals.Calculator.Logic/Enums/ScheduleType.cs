using System.ComponentModel.DataAnnotations;

namespace KBS.Portals.Calculator.Logic.Enums
{

    ///TODO: Get proper descriptions from Gary for the following.
    public enum ScheduleType
    {
        [Display(Description = "Documentation Fee")]
        DOC,
        [Display(Description = "??")]
        INS,
        [Display(Description = "??")]
        FEE,
        [Display(Description = "??")]
        HOL,
        [Display(Description = "Balance")]
        BAL,
        [Display(Description = "Residual")]
        RES,
        [Display(Description = "Purchase")]
        PUR,
        [Display(Description = "Upfront")]
        UPF,
        [Display(Description = "??")]
        LAD,
        [Display(Description = "??")]
        REN
    }
}