using Microsoft.AspNetCore.Identity;
using Shop.Common.Models.DTO.Auth;

namespace Shop.Blazor.Services.API.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task LogoutAsync();
    }
}
