using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> userClaims);
    }
}
