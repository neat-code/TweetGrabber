namespace TwitterAnalysis.Core.Models
{
    using Newtonsoft.Json;
    using System;

    public class TwitterSubject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string PartitionKey { get; set; }

        public DocumentType DocumentType { get; set; }

        public string Subject { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }
    }
}
