using MarvelAPI.Models.Users;

namespace MarvelAPI.Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model);
    }
}