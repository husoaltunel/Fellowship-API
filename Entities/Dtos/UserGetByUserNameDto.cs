using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserGetByUserNameDto : IDto
    {
        public string Username { get; set; }
    }
}
