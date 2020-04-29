using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterAnalysis.Core.Models.Tweet
{
    public class Urls
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; set; }

        [JsonProperty("indices")]
        public IEnumerable<int> Indices { get; set; }
    }
}
