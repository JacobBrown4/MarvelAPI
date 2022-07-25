using MarvelAPI.Models.Users;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarvelAPI.Services.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (await GetUserByEmailAsync(model.Email) != null || await GetUserByUsernameAsync(model.Username) != null)
            {
                return false;
            }
            
            var user = new UserEntity
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                DateCreated = DateTime.Now
            };

            _dbContext.Users.Add(user);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        private async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
        }

        private async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
        }
    }
}