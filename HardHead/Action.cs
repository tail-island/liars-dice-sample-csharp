using Newtonsoft.Json;

namespace HardHead
{
    public sealed class Action
    {
        [JsonProperty("bid")]
        public Bid Bid { get; set; }

        [JsonProperty("challenge")]
        public Challenge Challenge { get; set; }
    }
}
