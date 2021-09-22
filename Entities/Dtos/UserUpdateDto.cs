using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserUpdateDto : IDto
    {
        public int Id { get;set;}
        public string Username { get; set; }
        public string Password { get;set;}
        public string Email { get; set; }

    }
}
