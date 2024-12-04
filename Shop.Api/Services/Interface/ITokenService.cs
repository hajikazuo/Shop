using Microsoft.AspNetCore.Identity;
using Shop.Common.Models.DTO.Auth;
using Shop.Common.Models.Entities.Users;

namespace Shop.Api.Services.Interface
{
    public interface ITokenService
    {
        TokenResultDto CreateJwtToken(User user, IList<string> roles);
    }
}
