using Authentication.Domain.DTOs.User;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Repositories;
using Authentication.Shared;

namespace Authentication.Infrastructure.Repositories.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<bool> UserExist(string email);

        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDto userRequest);

        Task<ServiceResponse<string>> Login(string email, string password);

    }
}
