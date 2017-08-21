using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KBS.Portals.Calculator.Logic.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Frequency
    {
        None = 0,
        Monthly = 1,
        BiMonthly = 2,
        Quarterly = 3,
        HalfYearly = 6,
        Annual = 12
    }
}