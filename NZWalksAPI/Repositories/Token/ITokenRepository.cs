using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repositories.Token
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
