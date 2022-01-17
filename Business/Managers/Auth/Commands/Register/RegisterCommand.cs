using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Auth.Commands.Register
{
    public class RegisterCommand : RegisterDto,IRequest<IDataResult<UserDto>>
    {
        
    }
}
