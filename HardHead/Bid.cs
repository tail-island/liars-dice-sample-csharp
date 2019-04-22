using Newtonsoft.Json;

namespace HardHead
{
    // xがy以上
    public sealed class Bid
    {
        // 目
        [JsonProperty("face")]
        public int Face { get; set; }

        // 個数
        [JsonProperty("min_count")]
        public int MinCount { get; set; }
    }
}
