namespace FuFood.Services;

public class JwtOptions
{
    public const string SectionName = "JwtOptions";

    public string AccessTokenSigner { get; set; }
    public string Issuer { get; set; } = "https://api.fufood.jocelynh.me";
    public string Audience { get; set; } = "https://api.fufood.jocelynh.me";
    public int ExpirySeconds { get; set; } = 8 * 60 * 60;

    public byte[] AccessTokenSignerBytes => Convert.FromBase64String(AccessTokenSigner);
}