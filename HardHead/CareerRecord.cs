using Newtonsoft.Json;

namespace HardHead
{
    // 過去の戦歴のレコード
    public sealed class CareerRecord
    {
        // 当該ゲームでのID
        [JsonProperty("id")]
        public string Id { get; set; }

        // ゲーム
        [JsonProperty("game")]
        public Game Game { get; set; }
    }
}
