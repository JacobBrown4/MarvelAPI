using MarvelAPI.Models.Users;
using MarvelAPI.Data;
using MarvelAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

            var passwordHasher = new PasswordHasher<UserEntity>();

            user.Password = passwordHasher.HashPassword(user, model.Password);

            _dbContext.Users.Add(user);
            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<UserDetail>> GetAllUsersAsync()
        {
            var users = await _dbContext.Users
                .Select(
                    u => new UserDetail
                    {
                        Id = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        DateCreated = u.DateCreated
                    }
                )
                .ToListAsync();
                return users;
    }

        public async Task<UserDetail> GetUserByIdAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user is null)
            {
                return null;
            }

            var userDetail = new UserDetail
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateCreated = user.DateCreated
            };
            return userDetail;
        }

        public async Task<bool> UpdateUserAsync(int userId, UserUpdate request)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user is null)
            {
                return false;
            }

            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = request.Password;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user is null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            return await _dbContext.SaveChangesAsync() == 1;
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