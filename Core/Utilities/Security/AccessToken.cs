using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Security
{
    public class AccessToken
    {
        public string Token { get;set;}
        public DateTime Expiration { get; set; }
    }
}
