using Shop.Blazor.Models;

namespace Shop.Blazor.Services.Authentication
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel model);
        Task Logout();
    }
}
