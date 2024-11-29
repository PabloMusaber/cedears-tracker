namespace CEDEARsTracker.Models
{
    public class AuthConfig
    {
        public string AuthorizedClient { get; set; } = string.Empty;
        public string ClientKey { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
    }
}