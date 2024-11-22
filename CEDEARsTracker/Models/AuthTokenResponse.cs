namespace CEDEARsTracker.Models;

public class AuthTokenResponse
{
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public int Expires { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
}