using Authentication.Business._01_Common;
using Authentication.Domain.DTOs.User;
using Authentication.Shared;

namespace Authentication.Business.Users
{
    public interface IUserService : IService
    {
        Task<ServiceResponse<int>> Register(UserCreateDto user, string password);

        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDto userRequest);

        Task<ServiceResponse<string>> Login(string email, string password);
    }
}
