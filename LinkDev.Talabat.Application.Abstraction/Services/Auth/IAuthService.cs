using LinkDev.Talabat.Application.Abstraction.Models.Auth;

namespace LinkDev.Talabat.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto>  RegisterAsync(RegisterDto model);
        Task<UserDto>  LoginAsync(LoginDto model);

        // Task<bool> IsEmailExistAsync(string email);
    }
}
