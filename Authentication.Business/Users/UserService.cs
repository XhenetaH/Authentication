using Authentication.Domain.DTOs.User;
using Authentication.Domain.Entities;
using AutoMapper;
using Authentication.Infrastructure.Repositories.Users;
using Authentication.Shared;

namespace Authentication.Business.Users
{
    public class UserService : IUserService
    {
        #region Properties
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constuctor
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public Task<ServiceResponse<int>> Register(UserCreateDto user, string password)
        {
            var result = _userRepository.Register(_mapper.Map<User>(user), password);
            
            return Task.FromResult(result.Result);
        }
        public Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDto userRequest)
        {
            return _userRepository.ChangePassword(userRequest);
        }

        public Task<ServiceResponse<string>> Login(string email, string password)
        {
            return _userRepository.Login(email, password);
        }
        #endregion

    }
}
