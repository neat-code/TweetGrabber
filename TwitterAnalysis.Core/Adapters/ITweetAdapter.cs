namespace TwitterAnalysis.Core.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Tweetinvi.Models;
    using TwitterAnalysis.Core.Models;

    public interface ITweetAdapter
    {
        TweetDbObject GetDbObject(ITweet tweet, IEnumerable<string> topic);
    }

    public class TweetAdapter : ITweetAdapter
    {
        public TweetDbObject GetDbObject(ITweet tweet, IEnumerable<string> topics)
        {
            var timeStamp = DateTime.UtcNow;

            return new TweetDbObject
            {
                Id = Guid.NewGuid().ToString(),
                PartitionKey = timeStamp.ToString("yyyy-MM-dd") + "_" + topics.FirstOrDefault(),
                DocumentType = DocumentType.Tweet,
                Timestamp = timeStamp,
                Topics = topics,
                Tweet = tweet,
            };
        }
    }
}