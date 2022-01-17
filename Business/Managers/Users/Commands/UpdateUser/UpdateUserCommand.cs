using Business.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : UpdateUserDto, IRequest<IDataResult<int>>
    {

    }
}
