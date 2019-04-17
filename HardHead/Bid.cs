using Newtonsoft.Json;

namespace HardHead
{
    public sealed class Bid
    {
        [JsonProperty("face")]
        public int Face { get; set; }

        [JsonProperty("min_count")]
        public int MinCount { get; set; }
    }
}
