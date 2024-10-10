using Microsoft.AspNetCore.Identity;

namespace Shop.Api.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, IList<string> roles);
    }
}
