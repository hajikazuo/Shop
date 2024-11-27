using Microsoft.AspNetCore.Identity;
using Shop.Common.Models.Entities.Users;

namespace Shop.Api.Services.Interface
{
    public interface ITokenService
    {
        string CreateJwtToken(User user, IList<string> roles);
    }
}
