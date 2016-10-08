using Newtonsoft.Json;

namespace Nop.Plugin.Misc.LionParcel.Models
{
    public class TariffSearchRequest
    {
        public string Origin { get; set; }

        public string Destination { get; set; }

        public float Weight { get; set; }

        [JsonIgnore]
        public string Url { get; set; }
    }
}