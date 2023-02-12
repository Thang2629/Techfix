using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TechFix.EntityModels.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductAssociatedType
    {
        Manufacturer,
        ProductCondition,
        ProductUnit,
        Store,
        Supplier
    }
}
