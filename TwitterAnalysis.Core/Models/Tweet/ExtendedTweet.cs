namespace TwitterAnalysis.Core.Models.Tweet
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ExtendedTweet
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("full_text")]
        public string FullText { get; set; }

        [JsonProperty("display_text_range")]
        public IEnumerable<int> DisplayTextRange { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

    }
}
