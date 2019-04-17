using Newtonsoft.Json;

namespace HardHead
{
    public sealed class Career
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("career_records")]
        public CareerRecord[] CareerRecords { get; set; }
    }
}
