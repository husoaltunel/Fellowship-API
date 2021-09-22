﻿using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Managers.Account.Commands.Add
{
    public class UserAddCommand : UserRegisterDto,IRequest<IDataResult<UserViewDto>>
    {
        
    }
}
