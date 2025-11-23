using System.Security.Claims;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;

namespace FuFood.Services;

public class JwtService(IOptions<JwtOptions> options)
{
    private readonly JwtOptions _options = options.Value;

    public string IssueAccessTokenForUser(Guid userId)
    {
        return JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(_options.AccessTokenSignerBytes)
            .AddClaim("sub", userId.ToString())
            .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(_options.ExpirySeconds))
            .AddClaim("iss", _options.Issuer)
            .AddClaim("aud", _options.Audience)
            .Encode();
    }
}