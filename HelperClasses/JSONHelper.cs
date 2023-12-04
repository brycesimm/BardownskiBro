using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BardownskiBro.HelperClasses
{
    public static class JSONHelper
    {
        public static JObject? StringToJObject(string json)
        {
            if (json is not null)
            {
                return JObject.Parse(json);
            }
            return null;
        }
    }
}
