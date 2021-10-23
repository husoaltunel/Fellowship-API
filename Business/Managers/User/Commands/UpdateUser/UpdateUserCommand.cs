using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.User.Commands.UpdateUser
{
    public class UpdateUserCommand : UpdateUserDto, IRequest<IDataResult<int>>
    {

    }
}
