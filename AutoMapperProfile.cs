using AutoMapper;
using server_new_try.DTOs;
using Server_Try02.Models;

namespace Server_Try02
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel,GetUserDto>();
            CreateMap<RegisterUserDto,UserModel>();
            CreateMap<UserModel,UserHistModel>();
        }
    }
}