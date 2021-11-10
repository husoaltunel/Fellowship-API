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
            CreateMap<RegisterDto, User>().ForMember(
                dest=>dest.Created,
                opt => opt.MapFrom(src=> DateTime.Now)
                )
                .ForMember(
                dest => dest.LastActive,
                opt => opt.MapFrom(src => DateTime.Now)
                );
            CreateMap<User, LoginInfoDto>();



            CreateMap<UpdateUserDto, User>().ForMember(
                dest => dest.Age,
                opt => opt.MapFrom((src,dest) => src.DateOfBirth != null ? src.DateOfBirth.CalculateAge() : dest.Age )
                );

            CreateMap<User, UserDto>();
            CreateMap<Photo, PhotoDto>();
        }
    }
}
