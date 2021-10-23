using Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class GetUserByUsernameDto : IDto
    {
        public string Username { get;set;}
    }
}
