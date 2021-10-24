using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserLoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
