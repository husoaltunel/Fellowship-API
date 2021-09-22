using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Account.Queries.GetByUserNameForLogin
{
    public class UserGetByUserNameForLoginQuery : UserGetByUserNameDto, IRequest<IDataResult<User>>
    {
        
    }
}
