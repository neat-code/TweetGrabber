namespace TwitterAnalysis.Core.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using Tweetinvi.Models;

    public class TweetDbObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string PartitionKey { get; set; }

        public DocumentType DocumentType { get; set; }

        public IEnumerable<string> Topics { get; set; }

        public DateTime Timestamp { get; set; }

        public object Tweet { get; set; }
    }
}
