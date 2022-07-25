using MarvelAPI.Models.Users;

namespace MarvelAPI.Services.User
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model);
        Task<IEnumerable<UserDetail>> GetAllUsersAsync();
        Task<UserDetail> GetUserByIdAsync(int userId);
    }
}