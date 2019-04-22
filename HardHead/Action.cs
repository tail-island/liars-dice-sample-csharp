using Newtonsoft.Json;

namespace HardHead
{
    // 行動
    public sealed class Action
    {
        // xがy以上
        [JsonProperty("bid")]
        public Bid Bid { get; set; }

        // チャレンジ
        [JsonProperty("challenge")]
        public Challenge Challenge { get; set; }
    }
}
