using AutoMapper;
using Business.Managers.Auth.Commands.Register;
using Business.Managers.User.Commands.UpdateUser;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Hashing;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
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

            CreateMap<User,LoginInfoDto>();
               
            CreateMap<RegisterCommand,User>()
                .ForMember(
                dest => dest.Username,
                opt => opt.MapFrom(src => src.Username.ToLower())
                )
                .ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordHash())
                )
                .ForMember(
                dest => dest.PasswordSalt,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordSalt())
                );


            CreateMap<UpdateUserCommand, User>().ForMember(
                dest => dest.PasswordHash,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordHash())
                )
                .ForMember(
                dest => dest.PasswordSalt,
                opt => opt.MapFrom(src => HashingUtil.GetPasswordSalt())
                )
                .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth != null ? src.DateOfBirth.CalculateAge() : 0)
                );

            CreateMap<User,UserDto>();
            CreateMap<Photo,PhotoDto>();
        }
    }
}
