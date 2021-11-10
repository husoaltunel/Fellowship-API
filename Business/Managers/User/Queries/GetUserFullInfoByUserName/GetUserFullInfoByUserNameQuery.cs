using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.User.Queries.GetUserFullInfoByUserName
{
    public class GetUserFullInfoByUserNameQuery : GetUserByUsernameDto, IRequest<IDataResult<Core.Entities.Concrete.User>>
    {
        
    }
}
