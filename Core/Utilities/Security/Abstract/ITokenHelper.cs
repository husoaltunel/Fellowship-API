using Business.Entities.Concrete;
using Business.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Security.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> userClaims);
    }
}
