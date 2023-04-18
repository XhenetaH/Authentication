using Authentication.Domain.DTOs.User;
using Authentication.Domain.Entities;
using AutoMapper;

namespace Authentication.Business._00_Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User

            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();

            CreateMap<User, UserCreateDto>();

            CreateMap<UserCreateDto, User>();

            #endregion
        }
    }
}
