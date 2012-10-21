using Newtonsoft.Json.Serialization;
using WeenyBoard.Extensions;

namespace WeenyBoard
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToCamelCase();
        }
    }
}