using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace JewelShrinos.Infrastructure.Services;

public class GoogleTokenValidator
{
    private readonly IConfiguration _configuration;

    public GoogleTokenValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<GoogleJsonWebSignature.Payload> ValidateAsync(string idToken)
    {
        var clientId = _configuration["GoogleAuth:ClientId"]
            ?? throw new InvalidOperationException("GoogleAuth:ClientId no configurado.");

        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { clientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        if (payload.Issuer != "accounts.google.com" &&
            payload.Issuer != "https://accounts.google.com")
        {
            throw new InvalidOperationException("Issuer de Google inválido.");
        }

        return payload;
    }
}