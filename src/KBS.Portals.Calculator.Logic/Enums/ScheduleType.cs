using System.ComponentModel.DataAnnotations;

namespace KBS.Portals.Calculator.Logic.Enums
{

    ///TODO: Get proper descriptions from Gary for the following.
    public enum ScheduleType
    {
        [Display(Description = "Documentation Fee")]
        DOC,
        [Display(Description = "Installment")]
        INS,
        [Display(Description = "Fee")]
        FEE,
        [Display(Description = "Payment Holiday")]
        HOL,
        [Display(Description = "Balance Collection")]
        BAL,
        [Display(Description = "Residual Payment")]
        RES,
        [Display(Description = "Purchase Fee")]
        PUR,
        [Display(Description = "Upfront")]
        UPF,
        [Display(Description = "Leased Asset Displosal")]
        LAD,
        [Display(Description = "LAD Renewal Installment")]
        REN,
        [Display(Description = "Commission")]
        COM,
        [Display(Description = "Finance Amount")]
        FIN
    }
}