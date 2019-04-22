using Newtonsoft.Json;

namespace HardHead
{
    // 過去の戦歴
    public sealed class Career
    {
        // ID
        [JsonProperty("id")]
        public string Id { get; set; }

        // 戦歴のレコード
        [JsonProperty("career_records")]
        public CareerRecord[] CareerRecords { get; set; }
    }
}
