using Business.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserLikeDto : IDto
    {
        public List<UserDto> UsersLikedMe { get;set;}
        public List<UserDto> UsersILiked { get;set;}

        public UserLikeDto()
        {
            UsersLikedMe = new List<UserDto>();
            UsersILiked = new List<UserDto>();
        }
    }
}
