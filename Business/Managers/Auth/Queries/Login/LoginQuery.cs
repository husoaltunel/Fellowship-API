using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Auth.Queries.Login
{
    public class LoginQuery : LoginDto,IRequest<IDataResult<LoginInfoDto>>
    {

    }
}
