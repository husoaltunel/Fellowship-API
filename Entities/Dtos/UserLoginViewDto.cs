using Core.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserLoginViewDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AccessToken AccessToken { get; set; }

    }
}
