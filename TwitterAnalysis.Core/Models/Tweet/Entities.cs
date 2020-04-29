namespace TwitterAnalysis.Core.Models.Tweet
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Entities
    {
        [JsonProperty("urls")]
        public IEnumerable<Urls> Urls { get; set; }

        [JsonProperty("user_mentions")]
        public IEnumerable<UserMention> UserMentions { get; set; }

        [JsonProperty("hashtags")]
        public IEnumerable<Hashtag> Hashtags { get; set; }

        [JsonProperty("symbols")]
        public IEnumerable<string> Symbols { get; set; }

        [JsonProperty("media")]
        public object Media { get; set; }
    }
}
