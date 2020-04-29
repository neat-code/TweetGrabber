namespace TwitterAnalysis.Core.Models.User
{
    using Newtonsoft.Json;
    using System;

    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("following")]
        public bool Following { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("entities")]
        public object Entities { get; set; }

        [JsonProperty("notifications")]
        public bool Notifications { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps { get; set; }

        [JsonProperty("follow_request_sent")]
        public bool FollowRequestSent { get; set; }

        [JsonProperty("default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }

        [JsonProperty("listed_count")]
        public int ListedCount { get; set; }

        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonProperty("profile_sidebar_border_color")]
        public string SidebarBorderColor { get; set; }

        [JsonProperty("profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty("profile_background_color")]
        public string ProfilaBackgroundColor { get; set; }

        [JsonProperty("profile_background_image_url")]
        public string ProfileBackgroundImageUrl { get; set; }

        [JsonProperty("profile_background_image_url_https")]
        public string ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty("profile_banner_url")]
        public string ProfileBannerUrl { get; set; }

        [JsonProperty("profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty("profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty("utc_offset")]
        public object UtcOffset { get; set; }

        [JsonProperty("time_zone")]
        public object TimeZone { get; set; }

        [JsonProperty("withheld_in_countries")]
        public object WithheldInContries { get; set; }

        [JsonProperty("withheld_scope")]
        public object WithheldScope { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
    }
}
