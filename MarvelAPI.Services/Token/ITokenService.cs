using MarvelAPI.Models.Token;

namespace MarvelAPI.Services.Token
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync(TokenRequest model);
    }
}