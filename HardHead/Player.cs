using Newtonsoft.Json;

namespace HardHead
{
    public sealed class Player
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("faces")]
        public int[] Faces { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }
    }
}
