using System.Text.Json.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace FuFood.Services;

public class LineOAuthService(HttpClient client, IOptions<LineOAuthOptions> options)
{
    private readonly LineOAuthOptions _options = options.Value;
    public const string AuthorizeBaseUrl = "https://access.line.me/oauth2/v2.1/authorize";
    public const string GetTokenUrl = "https://api.line.me/oauth2/v2.1/token";

    public string GetAuthorizationUrl(string state)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["response_type"] = "code",
            ["client_id"] = _options.ClientId,
            ["redirect_uri"] = _options.CallbackUrl,
            ["state"] = state,
            ["scope"] = "profile openid"
        };
        return QueryHelpers.AddQueryString(AuthorizeBaseUrl, queryParams!);
    }

    public class IssueAccessTokenResponse
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; init; }
        [JsonPropertyName("id_token")] public string IdToken { get; init; }
        [JsonPropertyName("expires_in")] public int ExpiresIn { get; init; }
        [JsonPropertyName("refresh_token")] public string RefreshToken { get; init; }
    }

    public async Task<IssueAccessTokenResponse?> GetAccessToken(string code)
    {
        var payload = new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["code"] = code,
            ["redirect_uri"] = _options.CallbackUrl,
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret
        };
        var response = await client.PostAsync(GetTokenUrl, new FormUrlEncodedContent(payload));
        return await response.Content.ReadFromJsonAsync<IssueAccessTokenResponse>();
    }
}