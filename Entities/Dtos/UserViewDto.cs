using Core.Entities.Dtos;
using Core.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserViewDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
       
    }
}
