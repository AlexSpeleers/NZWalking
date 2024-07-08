using Microsoft.AspNetCore.Identity;

namespace NZWalking.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
