namespace TwitterAnalysis.Core.Models.Tweet
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using TwitterAnalysis.Core.Models.User;

    public class Tweet
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("full_text")]
        public string FullText { get; set; }

        [JsonProperty("display_text_range")]
        public IEnumerable<int> DisplayTextRange { get; set; }

        [JsonProperty("extended_tweet")]
        public ExtendedTweet ExtendedTweet { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("current_user_retweet")]
        public object CurrentUserRetweet { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("extended_entities")]
        public object ExtendedEntities { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("truncated")]
        public bool Truncated { get; set; }

        [JsonProperty("reply_count")]
        public int ReplyCount { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public long? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public long? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("quote_count")]
        public int QuoteCount { get; set; }

        [JsonProperty("quoted_status_id")]
        public long? QuotedStatusId { get; set; }

        [JsonProperty("quoted_status_id_str")]
        public string QuotedStatusIdStr { get; set; }

        [JsonProperty("quoted_status")]
        public Tweet QuotedStatus { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("retweeted_status")]
        public Tweet RetweetedStatus { get; set; }

        [JsonProperty("possibly_sensitive")]
        public bool PossiblySensitive { get; set; }

        [JsonProperty("Lang")]
        public int? Lang { get; set; }

        [JsonProperty("contributorIds")]
        public object ContributorIds { get; set; }

        [JsonProperty("contributors")]
        public object Contributors { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("place")]
        public object Place { get; set; }

        [JsonProperty("scopes")]
        public object Scopes { get; set; }

        [JsonProperty("filter_level")]
        public string FilterLevel { get; set; }

        [JsonProperty("withheld_copyright")]
        public bool WithheldCopyright { get; set; }

        [JsonProperty("withheld_in_countries")]
        public object WithheldInContries { get; set; }

        [JsonProperty("withheld_scope")]
        public object WithheldScope { get; set; }
    }
}
