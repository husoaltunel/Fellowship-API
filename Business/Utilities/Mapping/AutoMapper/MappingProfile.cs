using AutoMapper;
using Business.Managers.Account.Commands.Add;
using Business.Managers.Account.Commands.Update;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<User, UserViewDto>().ReverseMap();
            CreateMap<User,UserLoginViewDto>();
               
            CreateMap<UserAddCommand, User>()
                .ForMember(
                user => user.Username,
                opt => opt.MapFrom(src => src.Username.ToLower())
                )
                .ForMember(
                user => user.PasswordHash,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordHash())
                )
                .ForMember(
                user => user.PasswordSalt,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordSalt())
                );


            CreateMap<UserUpdateCommand, User>().ForMember(
                user => user.PasswordHash,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordHash())
                )
                .ForMember(
                user => user.PasswordSalt,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordSalt())
                );
        }
    }
}
