namespace TwitterAnalysis.Core.Models.Tweet
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UserMention
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("indices")]
        public IEnumerable<int> Indices { get; set; }
    }
}
