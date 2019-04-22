using Newtonsoft.Json;

namespace HardHead
{
    // プレイヤー
    public sealed class Player
    {
        // ID。追跡を困難にするために、プレイヤーが集められるたびに適当な値が割り振られます。
        [JsonProperty("id")]
        public string Id { get; set; }

        // 自分のサイコロの目
        [JsonProperty("faces")]
        public int[] Faces { get; set; }

        // 自分の行動の集合
        [JsonProperty("actions")]
        public Action[] Actions { get; set; }
    }
}
