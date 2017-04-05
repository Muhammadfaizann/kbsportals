using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KBS.Portals.Calculator.Logic.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Product
    {
        Lease,
        HirePurchase,
        PersonalLoan
    }
}