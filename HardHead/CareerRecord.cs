using Newtonsoft.Json;

namespace HardHead
{
    public sealed class CareerRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("game")]
        public Game Game { get; set; }
    }
}
