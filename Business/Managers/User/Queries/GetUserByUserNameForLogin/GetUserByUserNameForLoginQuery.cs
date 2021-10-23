using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.User.Queries.GetUserByUserNameForLogin
{
    public class GetUserByUserNameForLoginQuery : GetUserByUsernameDto, IRequest<IDataResult<Core.Entities.Concrete.User>>
    {
        
    }
}
