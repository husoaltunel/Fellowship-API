using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Users.Queries.GetUserByUserName
{
    public class GetUserByUserNameQuery : GetUserByUsernameDto, IRequest<IDataResult<UserDto>>
    {
       
    }
}
