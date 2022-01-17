using Business.Entities.Abstract;
using Business.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entities.Concrete
{
    public class User : BaseEntity , IEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get;set;}
        public byte [] PasswordSalt { get;set;}
        public DateTime? DateOfBirth { get;set;}
        public int? Age { get;set;}
        public string Gender { get;set;}
        public string Country { get;set;}
        public string City { get;set;}
        public string KnownAs { get;set;}
        public string Introduction { get;set;}
        public DateTime Created { get;set;}
        public DateTime LastActive { get;set;}
        public string LookingFor { get;set;}
        public string Interests { get;set;}
        public int AlbumId { get;set;}

    }
}
